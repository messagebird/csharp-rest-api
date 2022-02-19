using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MessageBird.Objects.Conversations
{
    public enum ContentType
    {
        [EnumMember(Value = "audio")] Audio,
        [EnumMember(Value = "file")] File,
        [EnumMember(Value = "hsm")] Hsm,
        [EnumMember(Value = "image")] Image,
        [EnumMember(Value = "location")] Location,
        [EnumMember(Value = "text")] Text,
        [EnumMember(Value = "video")] Video,
        [EnumMember(Value = "event")] Event,
        [EnumMember(Value = "whatsappSticker")] WhatsAppSticker,
    }

    public class Content
    {
        [JsonProperty("audio")]
        public MediaContent Audio {get;set;}
        
        [JsonProperty("file")]
        public MediaContent File {get;set;}
        
        [JsonProperty("hsm")]
        public HsmContent Hsm {get;set;}
        
        [JsonProperty("image")]
        public MediaContent Image {get;set;}
        
        [JsonProperty("location")]
        public LocationContent Location {get;set;}
        
        [JsonProperty("text")]
        public string Text {get;set;}
        
        [JsonProperty("video")]
        public MediaContent Video {get;set;}

        [JsonProperty("whatsappSticker")]
        public WhatsAppStickerContent WhatsAppSticker { get; set; }
    }
}