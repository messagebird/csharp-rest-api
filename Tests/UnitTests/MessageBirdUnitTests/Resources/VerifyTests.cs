using System;
using MessageBird.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class VerifyTests
    {
        [TestMethod]
        public void DeserializeAndSerialize()
        {
            const string CreateVerifyResponse = @"{
  'id': '4e213b01155d1e35a9d9571v00162985',
  'href': 'https://rest.messagebird.com/verify/4e213b01155d1e35a9d9571v00162985',
  'recipient': 31612345678,
  'reference': null,
  'messages': {
    'href': 'https://rest.messagebird.com/messages/31bce2a1155d1f7c1db9df6b32167259'
  },
  'status': 'sent',
  'createdDatetime': '2016-07-24T14:26:57+00:00',
  'validUntilDatetime': '2016-07-24T14:27:27+00:00',
}";

            Verify verify = new Verify();

            MessageBird.Resources.Verify resource = new  MessageBird.Resources.Verify(verify);

            resource.Deserialize(CreateVerifyResponse);

            Assert.AreEqual("4e213b01155d1e35a9d9571v00162985", verify.Id);
            Assert.AreEqual("https://rest.messagebird.com/messages/31bce2a1155d1f7c1db9df6b32167259", verify.Message.Href);
            Assert.AreEqual(31612345678, verify.Recipient);
            Assert.AreEqual("https://rest.messagebird.com/verify/4e213b01155d1e35a9d9571v00162985", verify.Href);
            Assert.AreEqual(null, verify.Reference);
            Assert.AreEqual(VerifyStatus.Sent, verify.Status);

            JsonConvert.DeserializeObject<Verify>(resource.Object.ToString());
        }

        [TestMethod]
        public void QueryString()
        {
            Verify verify;
            MessageBird.Resources.Verify resource;

            verify = new Verify("4e213b01155d1e35a9d9571v00162985", "1234");
            resource = new MessageBird.Resources.Verify(verify);

            Assert.IsTrue(resource.HasQueryString);
            Assert.AreEqual("token=1234", resource.QueryString);

            verify = new Verify("4e213b01155d1e35a9d9571v00162985", "12 34");
            resource = new MessageBird.Resources.Verify(verify);

            Assert.IsTrue(resource.HasQueryString);
            Assert.AreEqual("token=12%2034", resource.QueryString);

            verify = new Verify();
            resource = new MessageBird.Resources.Verify(verify);

            Assert.IsFalse(resource.HasQueryString);
            Assert.AreEqual("", resource.QueryString);
        }
    }
}
