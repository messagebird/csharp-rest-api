using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MessageBird.Objects;

namespace MessageBird
{
    [Obsolete("Use RequestValidator instead", false)]
    public class RequestSigner
    {
        private readonly byte[] _secret;

        /**
         * Constructs a new RequestSigner instance.
         *
         * @param key Signing key. Can be retrieved through
         *            https://dashboard.messagebird.com/developers/settings. This
         *            is NOT your API key.
         *
         * @see https://developers.messagebird.com/docs/verify-http-requests
         */
        [Obsolete("Use RequestValidator instead", false)]
        public RequestSigner(byte[] secret)
        {
            _secret = secret;
        }


        /**
         * Computes the signature for the provided request and determines whether
         * it matches the expected signature (from the raw MessageBird-Signature header).
         *
         * @param expectedSignature Signature from the MessageBird-Signature
         *                          header in its original base64 encoded state.
         * @param request Request containing the values from the incoming webhook.
         * @return True if the computed signature matches the expected signature.
         */
        [Obsolete("Use RequestValidator instead", false)]
        public bool IsMatch(string encodedSignature, Request request)
        {
            using (var base64Transform = new FromBase64Transform())
            {
                var signatureBytes = Encoding.ASCII.GetBytes(encodedSignature);
                var decodedSignature = base64Transform.TransformFinalBlock(signatureBytes, 0, signatureBytes.Length);

                return IsMatch(decodedSignature, request);
            }
        }

        /**
         * Computes the signature for the provided request and determines whether
         * it matches the expected signature
         *
         * @param expectedSignature Decoded (with base64) signature
         *                          from the MessageBird-Signature header
         * @param request Request containing the values from the incoming webhook.
         * @return True if the computed signature matches the expected signature.
         */
        [Obsolete("Use RequestValidator instead", false)]
        public bool IsMatch(byte[] expectedSignature, Request request)
        {
            var actualSignature = ComputeSignature(request);
            return expectedSignature.SequenceEqual(actualSignature);
        }

        /**
         * Computes the signature for a request instance.
         *
         * @param request Request to compute signature for.
         * @return HMAC-SHA2556 signature for the provided request.
         */
        private byte[] ComputeSignature(Request request)
        {
            var timestampAndQuery = request.Timestamp + '\n' + request.SortedQueryParameters() + '\n';
            var timestampAndQueryBytes = Encoding.UTF8.GetBytes(timestampAndQuery);
            var bodyHashBytes = SHA256.Create().ComputeHash(request.Data);

            var signPayload = new byte[timestampAndQueryBytes.Length + bodyHashBytes.Length];
            Array.Copy(timestampAndQueryBytes, signPayload, timestampAndQueryBytes.Length);
            Array.Copy(bodyHashBytes, 0, signPayload, timestampAndQueryBytes.Length, bodyHashBytes.Length);

            return new HMACSHA256(_secret).ComputeHash(signPayload, 0, signPayload.Length);
        }
    }
}
