using System.Text;
using MessageBird.Objects;

namespace MessageBird.Resources.Conversations
{
    public class ConversationsBaseLists<T> : ConversationsResource
    {
        public ConversationsBaseLists(string name, BaseList<T> attachedObject,bool useWhatsAppSandbox)
            : base(name, attachedObject, useWhatsAppSandbox)
        {
            //
        }


        public override string QueryString
        {
            get
            {
                var baseList = (BaseList<T>)Object;

                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(base.QueryString))
                {
                    builder.AppendFormat("{0}&", base.QueryString);
                }

                builder.AppendFormat("limit={0}", baseList.Limit);
                builder.AppendFormat("&");
                builder.AppendFormat("offset={0}", baseList.Offset);

                return builder.ToString();
            }
        }
    }
}
