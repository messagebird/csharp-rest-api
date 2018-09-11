﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using MessageBird.Objects;
using MessageBird.Resources;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class VoiceMessageTest
    {
        [TestMethod]
        public void DeserializeAndSerialize()
        {
            const string JsonResultFromCreateVoiceMessageExample = @"{
  'id':'955c3130353eb3dcd090294a42643365',
  'href':'https:\/\/rest.messagebird.com\/voicemessages\/955c3130353eb3dcd090294a42643365',
  'body':'This is a test message. The message is converted to speech and the recipient is called on his mobile.',
  'reference':null,
  'language':'en-gb',
  'voice':'female',
  'repeat':1,
  'ifMachine':'continue',
  'scheduledDatetime':null,
  'createdDatetime':'2014-08-13T10:28:29+00:00',
  'recipients':{
    'totalCount':1,
    'totalSentCount':1,
    'totalDeliveredCount':0,
    'totalDeliveryFailedCount':0,
    'items':[
      {
        'recipient':31612345678,
        'status':'calling',
        'statusDatetime':'2014-08-13T10:28:29+00:00'
      }
    ]
  }
}";
            var recipients = new Recipients();
            var voiceMessage = new VoiceMessage("", recipients);
            var voiceMessages = new VoiceMessages(voiceMessage);
            voiceMessages.Deserialize(JsonResultFromCreateVoiceMessageExample);

            var voiceMessageResult = voiceMessages.Object as VoiceMessage;

            string voiceMessageResultString = voiceMessageResult.ToString();

            JsonConvert.DeserializeObject<VoiceMessage>(voiceMessageResultString); // check if Deserialize/Serialize cycle works.
        }

        [TestMethod]
        public void DeserializeRecipientsAsMsisdnsArray()
        {
            var recipients = new Recipients();
            recipients.AddRecipient(31612345678);

            var voiceMessage = new VoiceMessage("Welcome to MessageBird", recipients);
            var voiceMessages = new VoiceMessages(voiceMessage);

            string serializedMessage = voiceMessages.Serialize();

            voiceMessages.Deserialize(serializedMessage);
        }

        [TestMethod]
        public void ReportUrl()
        {
            var recipients = new Recipients();
            recipients.AddRecipient(31612345678);
            var optionalArguments = new VoiceMessageOptionalArguments
            {
                ReportUrl = "https://example.com/voice-status",
            };

            var voiceMessage = new VoiceMessage("Body", recipients, optionalArguments);

            Assert.AreEqual(voiceMessage.ReportUrl, "https://example.com/voice-status");
        }
    }
}
