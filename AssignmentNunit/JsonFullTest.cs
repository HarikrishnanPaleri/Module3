using AssignmentNunit.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentNunit
{
    [TestFixture]
    internal class JsonFullTests : CoreCodes
    {

        [Test]
        [Order(1)]
        public void GetSingleUSer()
        {
            test = extent.CreateTest("Get single user");
            Log.Information("Get single user test started");
            var req = new RestRequest("posts/1", Method.Get);
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");

                var userData = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(userData);
                Log.Information("User Returned");
                Assert.That(userData.Id, Is.EqualTo(1));
                Log.Information("User ID Returned");
                Assert.That(userData.Title, Does.Contain("optio"));
                Log.Information("Title check completed");
                Log.Information("Get single user test passed all asserts");
                test.Pass("Get single user test passed all asserts");

            }
            catch (AssertionException)
            {
                test.Fail("Get singleUser test failed");
            }
        }
        [Test]
        [Order(2)]

        public void GetAllUser()
        {
            test = extent.CreateTest("Get all user");
            Log.Information("Get all user test started");
            var req = new RestRequest("posts", Method.Get);
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);
                Assert.NotNull(users);
                Log.Information("Users Returned");
                Console.WriteLine("get:" + response.Content);
                Log.Information("Get all users test passed all asserts");
                test.Pass("Get all users test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Get all users test failed");
            }
        }
        [Test]
        
        [Order(0)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Create user test started");
            var request = new RestRequest("posts", Method.Post);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 2, id = "Rocket", title = "ROcket", body = "asjkx" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API response: {response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");
                Assert.NotNull(user.Id);
                Log.Information("User Id is not null");
                Log.Information("Create users test passed all asserts");
                test.Pass("Create users test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Get all users test failed");
            }

        }
        [Test]
        [TestCase(1)]
        [Order(3)]
        public void UpdateUser(int postId)
        {
            test = extent.CreateTest("Update user");
            var request = new RestRequest("posts/"+postId, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { userId = 100, title = "RocketBoy", body = "asjkx" });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("Update users test passed all asserts");
                test.Pass("Update users test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Update users test failed");
            }

        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            var request = new RestRequest("posts/1", Method.Delete);
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                Assert.That(response.Content, Is.EqualTo("{}"));
                Log.Information("Empty response returned");
                Log.Information("Delete users test passed all asserts");
                test.Pass("Delete users test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Delete users test failed");
            }
        }

    }
}
