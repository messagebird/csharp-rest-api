using System;
using MessageBird.Utilities;
using Newtonsoft.Json;
using System.Globalization;

namespace MessageBird.Json.Converters
{
    public class RFC3339DateTimeConverter : JsonConverter
    {
        private const string format = "Y-m-d\\TH:i:sP";
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                DateTime dateTime = (DateTime)value;
                writer.WriteValue(dateTime.ToString(format));
            }
            else
            {
                throw new JsonSerializationException("Expected value of type 'DateTime'.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Type t = (ReflectionUtils.IsNullable(objectType))
                ? Nullable.GetUnderlyingType(objectType)
                : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
              return null;
            }

            if (reader.TokenType == JsonToken.Date)
            {
                return reader.Value;
            }

            if (reader.TokenType == JsonToken.String)
            {
                DateTime dateTime;
                if (DateTime.TryParseExact((string)existingValue, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
                else
                {
                    throw new JsonSerializationException("Invalid date time format.");
                }
            }
            else
            {
                throw new JsonSerializationException(String.Format("Unexpected token '{0}' when parsing date.", reader.TokenType));
            }
        }

        public override bool CanConvert(Type objectType)
        {
            Type t = (ReflectionUtils.IsNullable(objectType))
               ? Nullable.GetUnderlyingType(objectType)
               : objectType;

            return t == typeof(DateTime);
        }
    }
}
