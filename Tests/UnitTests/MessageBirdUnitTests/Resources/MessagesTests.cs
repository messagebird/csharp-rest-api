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
             * Results in this JSON string in RC4 which is valid according to http://jsonlint.com:
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
  "createdDatetime": "2014-08-11T13:18:53+02:00",
  "recipients": [
    31612345678
  ]
}             * Results in this JSON string in RC3 which is valid according to http://jsonlint.com:
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
             * Throwing this error in RC4:
Newtonsoft.Json.JsonSerializationException was unhandled by user code
  HResult=-2146233088
  Message=Cannot deserialize the current JSON array (e.g. [1,2,3]) into type 'MessageBird.Objects.Recipients' because the type requires a JSON object (e.g. {"name":"value"}) to deserialize correctly.
To fix this error either change the JSON to a JSON object (e.g. {"name":"value"}) or change the deserialized type to an array or a type that implements a collection interface (e.g. ICollection, IList) like List<T> that can be deserialized from a JSON array. JsonArrayAttribute can also be added to the type to force it to deserialize from a JSON array.
Path 'recipients', line 16, position 18.
  Source=Newtonsoft.Json
  StackTrace:
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.EnsureArrayContract(JsonReader reader, Type objectType, JsonContract contract)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateList(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, Object existingValue, String id)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, Object existingValue)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.Deserialize(JsonReader reader, Type objectType, Boolean checkAdditionalContent)
       at Newtonsoft.Json.Serialization.JsonSerializerProxy.DeserializeInternal(JsonReader reader, Type objectType)
       at Newtonsoft.Json.JsonSerializer.Deserialize(JsonReader reader, Type objectType)
       at Newtonsoft.Json.JsonSerializer.Deserialize[T](JsonReader reader)
       at MessageBird.Json.Converters.RecipientsConverter.ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer) in c:\ProgramData\jpluim54\Versioned\MessageBird.CSharp-Rest-Client\MessageBird\Json\Converters\RecipientsConverter.cs:line 27
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.DeserializeConvertable(JsonConverter converter, JsonReader reader, Type objectType, Object existingValue)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.SetPropertyValue(JsonProperty property, JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, Object target)
       at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PopulateObject(Object newObject, JsonReader reader, JsonObjectContract contract, JsonProperty member, String id)
  InnerException: 
             * Throwing this error in RC3:
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
