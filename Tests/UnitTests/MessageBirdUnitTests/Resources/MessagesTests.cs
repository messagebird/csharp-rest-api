using MessageBird.Objects;
using MessageBird.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class MessagesTests
    {
        [TestMethod]
        public void DeserializeAndSerialize()
        {
            const string JsonResultFromCreateMessage = @"{
  'id':'e7028180453e8a69d318686b17179500',
  'href':'https:\/\/rest.messagebird.com\/messages\/e7028180453e8a69d318686b17179500',
  'direction':'mt',
  'type':'sms',
  'originator':'MsgBirdSms',
  'body':'Welcome to MessageBird',
  'reference':null,
  'validity':null,
  'gateway':56,
  'typeDetails':{
    
  },
  'datacoding':'plain',
  'mclass':1,
  'scheduledDatetime':null,
  'createdDatetime':'2014-08-11T11:18:53+00:00',
  'recipients':{
    'totalCount':1,
    'totalSentCount':1,
    'totalDeliveredCount':0,
    'totalDeliveryFailedCount':0,
    'items':[
      {
        'recipient':31612345678,
        'status':'sent',
        'statusDatetime':'2014-08-11T11:18:53+00:00'
      }
    ]
  }
}";
            Recipients recipients = new Recipients();
            Message message = new Message("", "", recipients);
            Messages messages = new Messages(message);
            messages.Deserialize(JsonResultFromCreateMessage);

            Message messageResult = messages.Object as Message;

            string messageResultString = messageResult.ToString();
           
            JsonConvert.DeserializeObject<Message>(messageResultString); // check if Deserialize/Serialize cycle works.
        }

        [TestMethod]
        public void DeserializeRecipientsAsMsisdnsArray()
        {
            var recipients = new Recipients();
            recipients.AddRecipient(31612345678);

            var message = new Message("MsgBirdSms", "Welcome to MessageBird", recipients);
            var messages = new Messages(message);

            string serializedMessage = messages.Serialize();

            messages.Deserialize(serializedMessage);
        }

    }
}
