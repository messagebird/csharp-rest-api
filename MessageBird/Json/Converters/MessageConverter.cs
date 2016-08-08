using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using MessageBird.Objects;

namespace MessageBird.Json.Converters
{
    class MessageConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var message = value as Message;
            var toSerialize = new Dictionary<string, string>();

            if (message == null) {
                toSerialize.Add("href", null);
            } else {
                toSerialize.Add("href", message.Href);
            }
            serializer.Serialize(writer, toSerialize);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<Message>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Message).IsAssignableFrom(objectType);
        }
    }
}
