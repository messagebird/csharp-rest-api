namespace MessageBird.Objects
{
    public class Error
    {
        private int code;
        private string description;
        private string parameter;

        public Error(int code, string description, string parameter)
        {
            this.code = code;
            this.description = description;
            this.parameter = parameter;
        }
    }
}
