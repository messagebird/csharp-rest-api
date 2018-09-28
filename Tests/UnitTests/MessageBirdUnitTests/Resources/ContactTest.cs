using MessageBird;
using MessageBird.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdTests.Resources
{
    [TestClass]
    public class ContactTest
    {
        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"msisdn\":31612345678,\"firstName\":\"Foo\",\"lastName\":\"Bar\",\"custom1\":\"First\",\"custom2\":\"Second\"}")
                .AndReturns("{\"id\": \"id\",\"href\": \"https://rest.messagebird.com/contacts/id\",\"msisdn\": 31612345678,\"firstName\": \"Foo\",\"lastName\": \"Bar\",\"customDetails\": {\"custom1\": \"First\",\"custom2\": \"Second\",\"custom3\": null,\"custom4\": null},\"groups\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/id/groups\"},\"messages\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/id/messages\"},\"createdDatetime\": \"2018-08-10T13:58:00+00:00\",\"updatedDatetime\": \"2018-08-10T13:58:00+00:00\"}")
                .FromEndpoint("POST", "contacts")
                .Get();
            var client = Client.Create(restClient.Object);

            var optionalArguments = new ContactOptionalArguments
            {
                FirstName = "Foo",
                LastName = "Bar",
                Custom1 = "First",
                Custom2 = "Second",
            };
            var contact = client.CreateContact(31612345678L, optionalArguments);
            restClient.Verify();

            Assert.AreEqual(31612345678L, contact.Msisdn);
            Assert.AreEqual("Second", contact.CustomDetails.Custom2);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "contacts/some-id")
                .Get();
            var client = Client.Create(restClient.Object);

            client.DeleteContact("some-id");
            restClient.Verify();
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns("{\"offset\": 0,\"limit\": 20,\"count\": 2,\"totalCount\": 2,\"links\": {\"first\": \"https://rest.messagebird.com/contacts?offset=0\",\"previous\": null,\"next\": null,\"last\": \"https://rest.messagebird.com/contacts?offset=0\"},\"items\": [{\"id\": \"first-id\",\"href\": \"https://rest.messagebird.com/contacts/first-id\",\"msisdn\": 31612345678,\"firstName\": \"Foo\",\"lastName\": \"Bar\",\"customDetails\": {\"custom1\": null,\"custom2\": null,\"custom3\": null,\"custom4\": null},\"groups\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/first-id/groups\"},\"messages\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/first-id/messages\"},\"createdDatetime\": \"2018-07-13T10:34:08+00:00\",\"updatedDatetime\": \"2018-07-13T10:34:08+00:00\"},{\"id\": \"second-id\",\"href\": \"https://rest.messagebird.com/contacts/second-id\",\"msisdn\": 49612345678,\"firstName\": \"Hello\",\"lastName\": \"World\",\"customDetails\": {\"custom1\": null,\"custom2\": null,\"custom3\": null,\"custom4\": null},\"groups\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/second-id/groups\"},\"messages\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/second-id/messages\"},\"createdDatetime\": \"2018-07-13T10:33:52+00:00\",\"updatedDatetime\": null}]}")
                .FromEndpoint("GET", "contacts?limit=20&offset=0")
                .Get();
            var client = Client.Create(restClient.Object);

            var contactList = client.ListContacts();
            restClient.Verify();

            Assert.AreEqual(2, contactList.Count);
            Assert.AreEqual("https://rest.messagebird.com/contacts?offset=0", contactList.Links.First);
            Assert.AreEqual("first-id", contactList.Items[0].Id);
            Assert.AreEqual("https://rest.messagebird.com/contacts/second-id/messages", contactList.Items[1].MessageReference.Href);
        }

        [TestMethod]
        public void ListPagination()
        {
            var restClient = MockRestClient
                .ThatReturns("{}")
                .FromEndpoint("GET", "contacts?limit=10&offset=25")
                .Get();
            var client = Client.Create(restClient.Object);

            client.ListContacts(10, 25);
            restClient.Verify();
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns("{\"id\": \"contact-id\",\"href\": \"https://rest.messagebird.com/contacts/contact-id\",\"msisdn\": 31612345678,\"firstName\": \"Foo\",\"lastName\": \"Bar\",\"customDetails\": {\"custom1\": \"First\",\"custom2\": \"Second\",\"custom3\": \"Third\",\"custom4\": \"Fourth\"},\"groups\": {\"totalCount\": 3,\"href\": \"https://rest.messagebird.com/contacts/contact-id/groups\"},\"messages\": {\"totalCount\": 5,\"href\": \"https://rest.messagebird.com/contacts/contact-id/messages\"},\"createdDatetime\": \"2018-07-13T10:34:08+00:00\",\"updatedDatetime\": \"2018-07-13T10:44:08+00:00\"}")
                .FromEndpoint("GET", "contacts/contact-id")
                .Get();
            var client = Client.Create(restClient.Object);

            var contact = client.ViewContact("contact-id");
            restClient.Verify();

            Assert.AreEqual("contact-id", contact.Id);
            Assert.AreEqual(3, contact.GroupReference.TotalCount);
        }

        [TestMethod]
        public void Update()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"lastName\":\"SomeName\",\"custom4\":\"Fourth\"}")
                .AndReturns("{\"id\": \"id\",\"href\": \"https://rest.messagebird.com/contacts/id\",\"msisdn\": 31687654321,\"firstName\": \"Foo\",\"lastName\": \"SomeName\",\"customDetails\": {\"custom1\": \"First\",\"custom2\": \"Second\",\"custom3\": null,\"custom4\": \"Fourth\"},\"groups\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/id/groups\"},\"messages\": {\"totalCount\": 0,\"href\": \"https://rest.messagebird.com/contacts/id/messages\"},\"createdDatetime\": \"2018-08-10T13:58:00+00:00\",\"updatedDatetime\": \"2018-08-10T13:58:00+00:00\"}")
                .FromEndpoint("PATCH", "contacts/some-id")
                .Get();
            var client = Client.Create(restClient.Object);

            var optionalArguments = new ContactOptionalArguments
            {
                LastName = "SomeName",
                Custom4 = "Fourth",
            };
            var contact = client.UpdateContact("some-id", optionalArguments);
            restClient.Verify();

            Assert.AreEqual(31687654321L, contact.Msisdn);
            Assert.AreEqual("SomeName", contact.LastName);
            Assert.AreEqual("Fourth", contact.CustomDetails.Custom4);
        }
    }
}
