
using System.Text;

namespace MessageBird.Resources
{
    public class MessageLists : BaseLists<Objects.Message>
    {
        public MessageLists()
            : base("messages", new Objects.MessageList())
        {
        }

        public MessageLists(Objects.MessageList messageList) : base("messages", messageList) { }
        
        public override string QueryString
        {
            get
            {
                var baseList = (Objects.MessageList)Object;

                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(base.QueryString))
                {
                    builder.AppendFormat("{0}", base.QueryString);
                }

                if (baseList.Status != "") { 
                    builder.AppendFormat("&status={0}", baseList.Status);
                }

                return builder.ToString();
            }
        }
    }
}
