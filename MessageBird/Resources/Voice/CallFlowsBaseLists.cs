using System.Text;
using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class CallFlowsBaseLists : CallFlowsResource
    {
        public CallFlowsBaseLists(string name, CallFlowList attachedObject)
            : base(name, attachedObject)
        {
        }

        public override string QueryString
        {
            get
            {
                var baseList = (CallFlowList)Object;

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