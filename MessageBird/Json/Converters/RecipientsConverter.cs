using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using MessageBird.Objects;

namespace MessageBird.Json.Converters
{
    class RecipientsConverter : JsonConverter
    {
        /*
         * When serializing, the recipients list, by default, will be serialized as an array msisdns.
         * However, in some use cases we want to pass the recipients object as is in json.
         * To choose how we serialize the recipients, we check the SerializeMsisdnsOnly property of the
         * recipients object.
         */
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var recipients = value as Recipients;

            if (recipients == null)
            {
                writer.WriteNull();
                return;
            }

            if (recipients.SerializeMsisdnsOnly)
            {
                writer.WriteStartArray();
                foreach (Recipient recipient in recipients.Items)
                {
                    serializer.Serialize(writer, recipient.Msisdn);
                }
                writer.WriteEndArray();
            }
            else
            {
                serializer.Serialize(writer, recipients);
            }
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // The MessageBird endpoint will always send a recipients object.
            // However, when deserializing a serialized object with recipients we have to take into consideration
            // the two ways a recipients object can serialize.
            // See the WriteJson method for more information.
            if (reader.TokenType == JsonToken.StartArray)
            {
                var msisdns = serializer.Deserialize<List<long>>(reader);
                return new Recipients(msisdns);
            }
            if (reader.TokenType == JsonToken.StartObject)
            {
                return serializer.Deserialize<Recipients>(reader);
            }
            throw new JsonSerializationException(String.Format("Unexpected token '{0}' when parsing recipients.", reader.TokenType));
        }


        public override bool CanConvert(Type objectType)
        {
            return typeof(Recipients).IsAssignableFrom(objectType);
        }
    }
}
