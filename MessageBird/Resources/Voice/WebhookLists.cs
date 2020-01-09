using System;
using MessageBird.Objects.Voice;

namespace MessageBird.Resources.Voice
{
    public class WebhookLists : VoiceBaseLists<Webhook>
    {
        public WebhookLists()
            : base("webhooks", new WebhookList())
        { }

        public WebhookLists(Objects.Voice.WebhookList webhookList) : base("webhooks", webhookList) { }
    }
}
