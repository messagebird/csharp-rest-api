using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using JWT;
using JWT.Builder;
using MessageBird.Exceptions;

namespace MessageBird
{
    /// <summary>
    /// Validates request signature signed by MessageBird services.
    /// </summary>
    ///
    /// <see href="https://developers.messagebird.com/docs/verify-http-requests" />
    public class RequestValidator
    {
        private const string TokenIssuer = "MessageBird";
        private readonly JwtBuilder _jwtBuilder;

        /// <summary>
        /// It is recommended to not skip URL validation to ensure high security.
        /// but the ability to skip URL validation is necessary in some cases, e.g.
        /// your service is behind proxy or when you want to validate it yourself.
        /// Note that when true, no query parameters should be trusted.
        /// Defaults to false.
        /// </summary>
        private readonly bool _skipURLValidation;

        /// <summary>
        /// This constructor initializes a new RequestValidator instance.
        /// </summary>
        ///
        /// <param name="secret">signing key. Can be retrieved from https://dashboard.messagebird.com/developers/settings. This is NOT your API key.</param>
        /// <param name="skipURLValidation">whether url_hash claim validation should be skipped. Note that when true, no query parameters should be trusted.</param>
        /// <see href="https://developers.messagebird.com/docs/verify-http-requests" />
        public RequestValidator(string secret, bool skipURLValidation = false)
        {
            _jwtBuilder = JwtBuilder.Create()
                     .WithAlgorithmFactory(new JWT.Algorithms.HMACSHAAlgorithmFactory())
                     .WithSecret(secret)
                     .MustVerifySignature();
            _skipURLValidation = skipURLValidation;
        }

        /// <summary>
        /// Constructor that allows you to overwrite the dateTimeProvider for easier testing
        /// </summary>
        public RequestValidator(string secret, IDateTimeProvider dateTimeProvider) : this(secret)
        {
            _jwtBuilder = _jwtBuilder.WithDateTimeProvider(dateTimeProvider);
        }

        /// <summary>
        /// Validate JWT signature.
        /// This JWT is signed with a MessageBird account unique secret key, ensuring the request is from MessageBird and a specific account.
        /// The JWT contains the following claims:
        /// <list type="bullet">
        ///     <item>
        ///         <term>url_hash</term>
        ///         <description>the raw URL hashed with SHA256 ensuring the URL wasn't altered.</description>
        ///     </item>
        ///     <item>
        ///         <term>payload_hash</term>
        ///         <description>the raw payload hashed with SHA256 ensuring the payload wasn't altered.</description>
        ///     </item>
        ///     <item>
        ///         <term>jti</term>
        ///         <description>a unique token ID to implement an optional non-replay check (NOT validated by default).</description>
        ///     </item>
        ///     <item>
        ///         <term>nbf</term>
        ///         <description>the not before timestamp.</description>
        ///     </item>
        ///     <item>
        ///         <term>exp</term>
        ///         <description>the expiration timestamp is ensuring that a request isn't captured and used at a later time.</description>
        ///     </item>
        ///     <item>
        ///         <term>iss</term>
        ///         <description>the issuer name, always MessageBird.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// 
        /// <param name="signature">request signature taken from request header MessageBird-Signature-JWT.</param>
        /// <param name="url">full URL of the receiving endpoint, including scheme, host, path and query.</param>
        /// <param name="body">request body.</param>
        /// <returns>A dictionary representing decoded JWT signature token claims</returns>
        /// <exception cref="RequestValidationException">Throws if signature can not be verified.</exception>
        public IDictionary<string, string> ValidateSignature(string signature, string url, byte[] body)
        {
            IDictionary<string, string> payload;
            try
            {
                payload = _jwtBuilder.Decode<IDictionary<string, string>>(signature);
            }
            catch (Exception e)
            {
                throw new RequestValidationException(e.Message, e);
            }

            string[] requiredClaims = { "iss", "nbf", "exp" };
            foreach (string claim in requiredClaims)
            {
                if (payload.ContainsKey(claim)) continue;
                throw new RequestValidationException(String.Format("invalid jwt: claim {0} is missing", claim));
            }

            if (!payload["iss"].Equals(TokenIssuer))
            {
                throw new RequestValidationException("invalid jwt: claim iss has wrong value");
            }

            if (!_skipURLValidation)
            {
                string expectedURLHash = GetSHA256Hash(Encoding.ASCII.GetBytes(url));
                if (!payload["url_hash"].Equals(expectedURLHash))
                {
                    throw new RequestValidationException("invalid jwt: claim url_hash is invalid");
                }
            }

            bool bodyExist = body != null && body.Length > 0;
            bool payloadHashExist = payload.ContainsKey("payload_hash");
            if (!bodyExist && payloadHashExist)
            {
                throw new RequestValidationException("invalid jwt: claim payload_hash is set but actual payload is missing");
            }
            if (bodyExist && !payloadHashExist)
            {
                throw new RequestValidationException("invalid jwt: claim payload_hash is not set but payload is present");
            }
            if (bodyExist && !payload["payload_hash"].Equals(GetSHA256Hash(body)))
            {
                throw new RequestValidationException("invalid jwt: claim payload_hash is invalid");
            }

            return payload;
        }

        private static string GetSHA256Hash(byte[] input)
        {
            byte[] data = SHA256.Create().ComputeHash(input);

            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
