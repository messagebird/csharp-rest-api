using MessageBird.Exceptions;
using MessageBird.Objects;
using MessageBird.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class MessagesTests
    {
        [TestMethod]
        public void DeserializeAndSerialize()
        {
            const string CreateMessageResponseTemplate = @"{
  'id':'e7028180453e8a69d318686b17179500',
  'href':'https:\/\/rest.messagebird.com\/messages\/e7028180453e8a69d318686b17179500',
  'direction':'mt',
  'type':'sms',
  'originator':'$ORIGINATOR',
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

            messages.Deserialize(CreateMessageResponseTemplate.Replace("$ORIGINATOR", "Messagebird"));
            JsonConvert.DeserializeObject<Message>(messages.Object.ToString());

            messages.Deserialize(CreateMessageResponseTemplate.Replace("$ORIGINATOR", "3112345678"));
            JsonConvert.DeserializeObject<Message>(messages.Object.ToString());

            messages.Deserialize(CreateMessageResponseTemplate.Replace("$ORIGINATOR", "+3112345678"));
            JsonConvert.DeserializeObject<Message>(messages.Object.ToString());
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

        [TestMethod]
        public void OriginatorFormat()
        {
            //https://developers.messagebird.com/docs/messaging
            //ORIGINATOR The sender of the message. This can be a telephone number (including country code) or an alphanumeric string.
            //In case of an alphanumeric string, the maximum length is 11 characters.

            var recipients = new Recipients();
            recipients.AddRecipient(31612345678);

            var message = new Message("Originator", "This is a message from a valid originator with less or equal than 11 alphanumeric characters.", recipients);
            Assert.AreEqual("Originator", message.Originator);

            message = new Message("3197001234567890", "This is a message from a valid originator with numeric characters.", recipients);
            Assert.AreEqual("3197001234567890", message.Originator);

            message = new Message("Or igna t0r", "This is a message from a valid originator with alphanumeric characters and whitespaces and less or equal than 11 characters.", recipients);
            Assert.AreEqual("Or igna t0r", message.Originator);

            try
            {
                message = new Message("Originator ", "This is a message from an invalid originator with trailing whitespace.", recipients);
                Assert.Fail("Expected an exception, because the originator contains trailing whitespace!");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual("Originator can only contain numeric or whitespace separated alphanumeric characters.", e.Message);
            }

            try
            {
                message = new Message(" Originator", "This is a message from an inavlid originator with leading whitespace.", recipients);
                Assert.Fail("Expected an exception, because the originator contains leading whitespace!");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual("Originator can only contain numeric or whitespace separated alphanumeric characters.", e.Message);
            }

            try
            {
                message = new Message("OriginatorXL", "This is a message from an invalid originator with more than 11 alphanumeric characters.", recipients);
                Assert.Fail("Expected an exception, because the originator has more than 11 alphanumeric characters.");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual("Alphanumeric originator is limited to 11 characters.", e.Message);
            }
        }
    }
}
