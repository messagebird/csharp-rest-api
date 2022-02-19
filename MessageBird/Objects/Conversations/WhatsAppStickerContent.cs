using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageBird.Objects.Conversations
{
    public class WhatsAppStickerContent
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }
}
