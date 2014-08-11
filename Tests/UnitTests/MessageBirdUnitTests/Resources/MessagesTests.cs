using MessageBird.Objects;
using MessageBird.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageBirdTests.Resources
{
    [TestClass()]
    public class MessagesTests
    {
        [TestMethod()]
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
            /*
             * Results in this JSON string which is valid according to http://jsonlint.com:
{
  "gateway": 56,
  "id": "e7028180453e8a69d318686b17179500",
  "href": "https://rest.messagebird.com/messages/e7028180453e8a69d318686b17179500",
  "direction": "mt",
  "type": "sms",
  "originator": "MsgBirdSms",
  "body": "Welcome to MessageBird",
  "reference": null,
  "validity": null,
  "typeDetails": {},
  "datacoding": "plain",
  "mclass": 1,
  "scheduledDatetime": null,
  "createdDatetime": "2014-08-11T13:18",
  "recipients": [
    31612345678
  ]
}
             */
            JObject.Parse(messageResultString); // check if it is valid JSON: this succeeds
            JsonConvert.DeserializeObject<Message>(messageResultString); // check if Deserialize/Serialize cycle works.
            /*
             * Throwing this error:
Newtonsoft.Json.JsonSerializationException was unhandled by user code
  HResult=-2146233088
  Message=Unexpected token 'String' when parsing date.
  Source=MessageBird
  StackTrace:
       at MessageBird.Json.Converters.RFC3339DateTimeConverter.ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer) in c:\ProgramData\jpluim54\Versioned\MessageBird.CSharp-Rest-Client\MessageBird\Json\Converters\RFC3339DateTimeConverter.cs:line 42
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.DeserializeConvertable(JsonConverter converter, JsonReader reader, Type objectType, Object existingValue)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.SetPropertyValue(JsonProperty property, JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, Object target)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PopulateObject(Object newObject, JsonReader reader, JsonObjectContract contract, JsonProperty member, String id)
  InnerException: 

             */
        }
    }
}
