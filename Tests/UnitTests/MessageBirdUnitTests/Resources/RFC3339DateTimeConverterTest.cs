using MessageBird.Exceptions;
using MessageBird.Objects;
using MessageBird.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class RFC3339DateTimeConverterTest
    {
        [TestMethod]
        public void InvalidRFC3339DateTime()
        {
            // The following message has an invalid createDateTime format.
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
  'createdDatetime':'2014-08-11T11:18:53',
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
            var recipients = new Recipients();
            var message = new Message("", "", recipients);
            var messages = new Messages(message);
            try
            {
                messages.Deserialize(JsonResultFromCreateMessage);
                Assert.Fail("Expected an error exception, because there is an invalid rfc3339 datetime.");
            }
            catch (ErrorException e)
            {
                // The exception is thrown by the RFC3339DateTimeConverter, so the inner exception
                // must be of type JsonSerializationException.
                Assert.IsInstanceOfType(e.InnerException, typeof(JsonSerializationException));
            }
            
        }
    }
}
