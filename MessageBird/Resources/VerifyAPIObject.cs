using System;
using System.ComponentModel;
using MessageBird.Json.Converters;
using MessageBird.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageBird.Resources
{
    internal class VerifyAPIObject : IIdentifiable<string>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("recipient")]
        public string Recipient { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("messages"), JsonConverter(typeof(MessageConverter))]
        public Message Message { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(StringEnumConverter))]
        public VerifyStatus? Status { get; set; }

        [JsonProperty("createdDatetime")]
        public DateTime? Created { get; set; }

        [JsonProperty("validUntilDatetime")]
        public DateTime? ValidUntil { get; set; }

        [JsonProperty("originator")]
        public string Originator { get; set; }

        [JsonProperty("template")]
        public string Template { get; set; }

        [JsonProperty("datacoding"), DefaultValue(DataEncoding.Plain), JsonConverter(typeof(StringEnumConverter))]
        public DataEncoding Encoding { get; set; }

        [JsonProperty("tokenLength"), DefaultValue(6)]
        public int TokenLength { get; set; }

        [JsonProperty("type"), DefaultValue(MessageType.Sms), JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }

        [JsonProperty("timeout"), DefaultValue(30)]
        public int Timeout { get; set; }

        [JsonProperty("voice"), JsonConverter(typeof(StringEnumConverter))]
        public Objects.Common.Voice Voice { get; set; }

        [JsonProperty("language"), JsonConverter(typeof(StringEnumConverter))]
        public  Objects.Common.Language Language { get; set; }

        [JsonProperty("Token")]
        public string Token { get;  set; }
    }
}