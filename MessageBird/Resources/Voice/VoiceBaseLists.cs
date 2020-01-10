using System.Text;
using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class VoiceBaseLists<T> : VoiceBaseResource<T>
    {
        public VoiceBaseLists(string name, VoiceBaseList<T> attachedObject)
            : base(name, attachedObject)
        {
        }

        public override string QueryString
        {
            get
            {
                var baseList = (VoiceBaseList<T>)Object;

                var builder = new StringBuilder();

                if (!string.IsNullOrEmpty(base.QueryString))
                {
                    builder.AppendFormat("{0}&", base.QueryString);
                }

                builder.AppendFormat("limit={0}", baseList.Limit);
                builder.AppendFormat("&");
                builder.AppendFormat("page={0}", baseList.Page);

                return builder.ToString();
            }
        }
    }
} 