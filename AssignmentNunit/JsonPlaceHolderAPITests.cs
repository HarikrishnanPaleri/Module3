using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNunit
{
    [TestFixture]
    internal class JsonPlaceHolderAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(1)]
        public void GetSingleUSer()
        {
            var req = new RestRequest("posts/1", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));


            var userData = JsonConvert.DeserializeObject<UserData>(response.Content);
            

            Assert.NotNull(userData);
            Assert.That(userData.Id, Is.EqualTo(1));
            Assert.That(userData.Title,Does.Contain("optio"));
        }
        [Test]
        [Order(2)]

        public void GetAllUser()
        {
            var req = new RestRequest("posts", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            List<UserData> users= JsonConvert.DeserializeObject<List<UserData>>(response.Content);
            Assert.NotNull(users);
            Console.WriteLine("get:" + response.Content);
        }
        [Test]
        [Order(0)]
        public void CreateUser()
        {
            var request = new RestRequest("posts", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "1", id = "Rocket",title="ROcket",body="asjkx" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);

        }
        [Test]
        [Order(3)]
        public void UpdateUser()
        {
            var request = new RestRequest("posts/1", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = "1", id = "Rocket", title = "RocketBoy", body = "asjkx" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);

        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            var request = new RestRequest("posts/1", Method.Delete);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        }
        
    }
}
    