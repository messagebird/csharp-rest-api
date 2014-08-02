using System;
using MessageBird.Objects;
using MessageBird.Utilities;
using Newtonsoft.Json;

namespace MessageBird.Json.Converters
{
    public class DirectionEnumConverter : JsonConverter
    {
        public DirectionEnumConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Direction direction = (Direction)value;
            switch (direction)
            {
                case Direction.MobileTerminated:
                    writer.WriteValue("mt");
                    break;
                case Direction.MobileOriginated:
                    writer.WriteValue("mo");
                    break;
                default:
                    throw new JsonSerializationException("Unexpected message direction!");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? true : false;
            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!isNullable)
                    throw new JsonSerializationException("Cannot convert null value to direction.");

                return null;
            }

            try
            {
                if (reader.TokenType == JsonToken.String)
                {
                    string directionText = reader.Value.ToString();
                    switch(directionText)
                    {
                        case "mt":
                            return Direction.MobileTerminated;
                        case "mo":
                            return Direction.MobileOriginated;
                        case "":
                            if (isNullable)
                            {
                                return null;
                            }
                            else
                            {
                                throw new JsonSerializationException("Cannot convert empty string to direction.");
                            }
                        default:
                            throw new JsonSerializationException("Invalid direction value.");

                    }
                }
                else
                {
                    throw new JsonSerializationException(String.Format("Unexpected token {0} when parsing direction.", reader.TokenType));
                }
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException(String.Format("Error converting value {0} to direction.", reader.Value), ex);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            Type t = (ReflectionUtils.IsNullable(objectType))
               ? Nullable.GetUnderlyingType(objectType)
               : objectType;

            return t == typeof(Direction);
        }
    }
}
