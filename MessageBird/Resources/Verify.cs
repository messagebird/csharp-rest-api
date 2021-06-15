using System;
using MessageBird.Exceptions;
using MessageBird.Objects;
using Newtonsoft.Json;

namespace MessageBird.Resources
{
    public sealed class Verify : Resource
    {
        public Verify(Objects.Verify verify)
            : base("verify", verify)
        {
        }

        private string Token
        {
            get
            {
                return Object != null ? ((Objects.Verify)Object).Token : null;
            }
        }

        private bool HasToken
        {
            get
            {
                return !string.IsNullOrEmpty(Token);
            }
        }

        public override string QueryString
        {
            get
            {
                return HasToken ? "token=" + System.Uri.EscapeDataString(Token) : string.Empty;
            }
        }
        
        public override void Deserialize(string resource)
        {
            if (Object == null)
            {
                throw new ErrorException("Invalid resource, has no attached object", new NullReferenceException());
            }

            try
            {
                var verifyApiObject = new VerifyAPIObject();
                JsonConvert.PopulateObject(resource, verifyApiObject);
                FromAPIToSDK((Objects.Verify)Object, verifyApiObject);
            }
            catch (JsonSerializationException e)
            {
                throw new ErrorException("Received response in an unexpected format!", e);
            }
        }

        public override string Serialize()
        {
            var settings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore};
            var verifyApiObject = FromSDKToAPI((Objects.Verify)Object);
            return JsonConvert.SerializeObject(verifyApiObject, settings);
        }
        
        private static VerifyAPIObject FromSDKToAPI(Objects.Verify verify)
        {
            return new VerifyAPIObject
            {
                Id = verify.Id,
                Href = verify.Href,
                Recipient = verify.Recipient == 0 ? verify.RecipientEmail : verify.Recipient.ToString(),
                Reference = verify.Reference,
                Message = verify.Message,
                Status = verify.Status,
                Created = verify.Created,
                ValidUntil = verify.ValidUntil,
                Originator = verify.Originator,
                Template  = verify.Template,
                Encoding  = verify.Encoding,
                TokenLength = verify.TokenLength,
                Type = verify.Type,
                Timeout = verify.Timeout,
                Voice = verify.Voice,
                Language  = verify.Language,
                Token  = verify.Token
            };
        }
        
        private static void FromAPIToSDK(Objects.Verify verify, VerifyAPIObject verifyApiObject)
        {
            long recipient;
            string recipientEmail = null;

            if (!long.TryParse(verifyApiObject.Recipient, out recipient))
            {
                recipientEmail = verifyApiObject.Recipient;
            }
            
            verify.Id = verifyApiObject.Id;
            verify.Href = verifyApiObject.Href;
            verify.Recipient = recipient;
            verify.RecipientEmail = recipientEmail;
            verify.Reference = verifyApiObject.Reference;
            verify.Message = verifyApiObject.Message;
            verify.Status = verifyApiObject.Status;
            verify.Created = verifyApiObject.Created;
            verify.ValidUntil = verifyApiObject.ValidUntil;
            verify.Originator = verifyApiObject.Originator;
            verify.Template = verifyApiObject.Template;
            verify.Encoding = verifyApiObject.Encoding;
            verify.TokenLength = verifyApiObject.TokenLength;
            verify.Type = verifyApiObject.Type;
            verify.Timeout = verifyApiObject.Timeout;
            verify.Voice = verifyApiObject.Voice;
            verify.Language = verifyApiObject.Language;
        }
    }
}
