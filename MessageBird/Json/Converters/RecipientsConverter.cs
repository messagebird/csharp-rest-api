using System;

using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Json.Converters
{
    class RecipientsConverter : JsonConverter
    {
        /*
         * When serializing, the recipients list acts as an array of msisdns. 
         */
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Recipients recipients = value as Recipients;
            writer.WriteStartArray();
            foreach (Recipient recipient in recipients.Items)
            {
                serializer.Serialize(writer, recipient.Msisdn);
            }
            writer.WriteEndArray();
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Recipients>(reader);
        }


        public override bool CanConvert(Type objectType)
        {
            return typeof(Recipients).IsAssignableFrom(objectType);
        }
    }
}
