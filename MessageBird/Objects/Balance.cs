using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Objects
{
    public enum PaymentMethod
    {
        [EnumMember(Value = "prepaid")]
        Prepaid,
        [EnumMember(Value = "postpaid")]
        Postpaid
    }

    public class Balance : IIdentifiable<string>
    {
        /// <summary>
        /// To uniformly treat objects, we implement the IIdentifiable interface even though
        /// a Balance object doesn't have an id!
        /// By returning null, the Balance object signals the user of a balance object
        /// (currently always the balance resource) to ignore the id property.
        /// </summary>
        /// <remarks>Throwing an exception will interfer with serialization of a balance object.</remarks>
        public string Id
        {
            get
            {
                return null;
            }
        }

        [JsonProperty("payment"), JsonConverter(typeof(StringEnumConverter))]
        public PaymentMethod Payment { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
