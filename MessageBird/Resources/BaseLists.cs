using MessageBird.Objects;
using System.Text;

namespace MessageBird.Resources
{
    public abstract class BaseLists<T> : Resource
    {
        public BaseLists(string name, BaseList<T> attachedObject)
            : base(name, attachedObject)
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
