using MessageBird;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class GroupTest
    {
        [TestMethod]
        public void Create()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"name\":\"Friends\"}")
                .AndReturns("{\"id\": \"group-id\",\"href\": \"https://rest.messagebird.com/groups/group-id\",\"name\": \"Friends\",\"contacts\": {\"totalCount\": 3,\"href\": \"https://rest.messagebird.com/groups/group-id\"},\"createdDatetime\": \"2018-07-25T12:16:10+00:00\",\"updatedDatetime\": \"2018-07-25T12:16:23+00:00\"}")
                .FromEndpoint("POST", "groups")
                .Get();
            var client = Client.Create(restClient.Object);

            var group = client.CreateGroup("Friends");
            restClient.Verify();

            Assert.AreEqual("group-id", group.Id);
            Assert.AreEqual(3, group.Contacts.TotalCount);
        }

        [TestMethod]
        public void Delete()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "groups/foo-id")
                .Get();
            var client = Client.Create(restClient.Object);

            client.DeleteGroup("foo-id");
            restClient.Verify();
        }

        [TestMethod]
        public void List()
        {
            var restClient = MockRestClient
                .ThatReturns("{\"offset\": 0,\"limit\": 20,\"count\": 2,\"totalCount\": 2,\"links\": {\"first\": \"https://rest.messagebird.com/groups?offset=0&limit=20\",\"previous\": null,\"next\": null,\"last\": \"https://rest.messagebird.com/groups?offset=0&limit=20\"},\"items\": [{\"id\": \"first-id\",\"href\": \"https://rest.messagebird.com/groups/first-id\",\"name\": \"First\",\"contacts\": {\"totalCount\": 3,\"href\": \"https://rest.messagebird.com/groups/first-id/contacts\"},\"createdDatetime\": \"2018-07-25T11:47:42+00:00\",\"updatedDatetime\": \"2018-07-25T14:03:09+00:00\"},{\"id\": \"second-id\",\"href\": \"https://rest.messagebird.com/groups/second-id\",\"name\": \"Second\",\"contacts\": {\"totalCount\": 4,\"href\": \"https://rest.messagebird.com/groups/second-id/contacts\"},\"createdDatetime\": \"2018-07-25T11:47:39+00:00\",\"updatedDatetime\": \"2018-07-25T14:03:09+00:00\"}]}")
                .FromEndpoint("GET", "groups?limit=20&offset=0")
                .Get();
            var client = Client.Create(restClient.Object);

            var groups = client.ListGroups();
            restClient.Verify();

            Assert.AreEqual(2, groups.TotalCount);
            Assert.AreEqual("https://rest.messagebird.com/groups?offset=0&limit=20", groups.Links.Last);
            Assert.AreEqual(3, groups.Items[0].Contacts.TotalCount);
        }

        [TestMethod]
        public void ListPagination()
        {
            var restClient = MockRestClient
                .ThatReturns("{}")
                .FromEndpoint("GET", "groups?limit=50&offset=10")
                .Get();
            var client = Client.Create(restClient.Object);

            client.ListGroups(50, 10);
            restClient.Verify();
        }

        [TestMethod]
        public void Update()
        {
            var restClient = MockRestClient
                .ThatExpects("{\"name\":\"family\"}")
                .AndReturns("{\"id\": \"group-id\",\"href\": \"https://rest.messagebird.com/groups/group-id\",\"name\": \"family\",\"contacts\": {\"totalCount\": 3,\"href\": \"https://rest.messagebird.com/groups/group-id\"},\"createdDatetime\": \"2018-07-25T12:16:10+00:00\",\"updatedDatetime\": \"2018-07-25T12:16:23+00:00\"}")
                .FromEndpoint("PATCH", "groups/group-id")
                .Get();
            var client = Client.Create(restClient.Object);

            var group = client.UpdateGroup("group-id", "family");
            restClient.Verify();

            Assert.IsNotNull(group.Id);
        }

        [TestMethod]
        public void View()
        {
            var restClient = MockRestClient
                .ThatReturns("{\"id\": \"group-id\",\"href\": \"https://rest.messagebird.com/groups/group-id\",\"name\": \"Friends\",\"contacts\": {\"totalCount\": 3,\"href\": \"https://rest.messagebird.com/groups/group-id\"},\"createdDatetime\": \"2018-07-25T12:16:10+00:00\",\"updatedDatetime\": \"2018-07-25T12:16:23+00:00\"}")
                .FromEndpoint("GET", "groups/group-id")
                .Get();
            var client = Client.Create(restClient.Object);

            var group = client.ViewGroup("group-id");
            restClient.Verify();

            Assert.AreEqual("group-id", group.Id);
            Assert.AreEqual("Friends", group.Name);
        }

        [TestMethod]
        public void AddContactsToGroup()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("GET", "groups/group-id?_method=PUT&ids[]=foo&ids[]=bar")
                .Get();
            var client = Client.Create(restClient.Object);

            client.AddContactsToGroup("group-id", new[] { "foo", "bar" });
            restClient.Verify();
        }

        [TestMethod]
        public void RemoveContactFromGroup()
        {
            var restClient = MockRestClient
                .ThatReturns(string.Empty)
                .FromEndpoint("DELETE", "groups/group-id/contacts/contact-id")
                .Get();
            var client = Client.Create(restClient.Object);

            client.RemoveContactFromGroup("group-id", "contact-id");
            restClient.Verify();
        }
    }
}
