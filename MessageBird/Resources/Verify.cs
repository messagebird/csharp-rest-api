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
                return (Object != null) ? ((Objects.Verify)Object).Token : null;
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
    }
}
