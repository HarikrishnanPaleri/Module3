using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using NUnit.Framework;
using RestExNunit.Utilities;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExNunit
{
    internal class ReqResTest
    {
        [TestFixture]
        internal class ReqResAPITests :CoreCodes
        {
            
           
            [Test]
            [Order(1)]
            public void GetSingleUSer()
            {
                test = extent.CreateTest("Get single user");
                Log.Information("Get single user test started");
                var req = new RestRequest("users/2", Method.Get);
                var response = client.Execute(req);
                try
                {
                    Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                    Log.Information($"API response: {response.Content}");


                    var userData = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
                    UserData? user = userData?.Data;

                    Assert.NotNull(user);
                    Log.Information("User Returned");
                    Assert.That(user.Id, Is.EqualTo(2));
                    Log.Information("User Id matches with fetch");
                    Assert.IsNotEmpty(user.Email);
                    Log.Information("Email is not empty");
                    Log.Information("Get single user test passed all asserts");
                    test.Pass("Get single user test passed all asserts");
                }
                catch (AssertionException)
                {
                    test.Fail("Get singleUser test failed");

                }
            }
            [Test]
            [Order(0)]
            public void CreateUser()
            {
                test = extent.CreateTest("Create user");
                Log.Information("Create user test started");
                var request = new RestRequest("users", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new { name = "Adithyan", job = "Rocket" });
                var response = client.Execute(request);
                try
                {
                    Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                    Log.Information($"API response: {response.Content}");
                    var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                    Assert.NotNull(user);
                    Log.Information("User Returned");
                    Assert.IsNull(user.Email);
                    Log.Information("User Email is null");

                    test.Pass("Create user test passed all asserts");
                }
                catch (AssertionException) 
                {
                    test.Fail("Create User test failed");
                }
            }
            [Test]
            [Order(2)]
            public void UpdateUser()
            {
                test = extent.CreateTest("Update user");
                Log.Information("Update user test started");
                var request = new RestRequest("users/2", Method.Put);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new { name = "Rocket-Adithyan", job = "Rocket" });
                var response = client.Execute(request);
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                try
                {
                    Assert.NotNull(user);
                    Log.Information("User Returned");
                    Assert.IsNull(user.Email);
                    Log.Information("User Email is not null");
                    test.Pass("Update user test passed all asserts");
                }
                catch (AssertionException) 
                {
                    test.Fail("Update User test failed");
                }
            }
            [Test]
            [TestCase(2)]
            [Order(3)]
            public void DeleteUser(int usrid)
            {
                test = extent.CreateTest("Delete user");
                Log.Information("Delete user test started");
                var request = new RestRequest("users/"+usrid, Method.Delete);
                var response = client.Execute(request);
                try
                {
                    Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                    Log.Information($"API response: {response.Content}");
                    Log.Information("No Content displayed");
                    test.Pass("Delete user test passed all asserts");
                }
                catch
                {
                    test.Fail("Delete User test failed");
                }
            }
            [Test]
            [Order(4)]
            public void GetNonExistingUser()
            {
                test = extent.CreateTest("Get Non Existing user");
                Log.Information("Get Non Existing user test started");
                var request = new RestRequest("users/999", Method.Get);
                var response = client.Execute(request);
                try
                {
                    Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                    Log.Information($"API response: {response.Content}");
                    Log.Information("No User found");
                    test.Pass("Get Non Existing user test passed all asserts");
                }
                catch(AssertionException) 
                {
                    test.Fail("Get Non existing User test failed");
                }

            }


        }
    }
}
