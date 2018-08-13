using Newtonsoft.Json;
using System.Collections.Generic;

namespace MessageBird.Objects
{
    /// <summary>
    /// A standard list as returned by most APIs/endpoints.
    /// </summary>
    /// <typeparam name="T">Type of this list's objects.</typeparam>
    public abstract class BaseList<T> : IIdentifiable<string>
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }

        [JsonProperty("items")]
        public IList<T> Items { get; set; }

        public virtual string Id
        {
            get
            {
                // We're only defining this as we need to implement
                // IIdentifiable. Should not be invoked typically. Concrete
                // lists are free to override this behavior.
                return string.Empty;
            }
        }
    }
}
