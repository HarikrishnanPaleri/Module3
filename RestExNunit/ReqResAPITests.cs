using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNunit
{
    [TestFixture]
    internal class ReqResAPITests
    {
        private RestClient client;
        private string baseUrl = "https://reqres.in/api/";

        [SetUp]
        public void Setup()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(1)]
        public void GetSingleUSer()
        {
            var req = new RestRequest("users/2", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));


            var userData = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
            UserData? user = userData?.Data;

            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Email);
        }
        [Test]
        [Order(0)]
        public void CreateUser()
        {
            var request = new RestRequest("users", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Adithyan", job = "Rocket" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode,Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
         
        }
        [Test]
        [Order(2)]
        public void UpdateUser()
        {
            var request = new RestRequest("users/2", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = "Rocket-Adithyan", job = "Rocket" });
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
          
        }
        [Test]
        [Order(3)]
        public void DeleteUser()
        {
            var request = new RestRequest("users/2", Method.Delete);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
        }
        [Test]
        [Order(4)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("users/999", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }


    }
}
