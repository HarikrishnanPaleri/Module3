using CaseStudy.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

namespace CaseStudy
{

    [TestFixture]
    internal class BookingAPITests : CoreCodes
    {

        [Test]
        [TestCase(2)]
        [Order(2)]
        public void GetBookingById(int bookinId)
        {
            test = extent.CreateTest("Get Booking By Id");
            Log.Information("Get Booking by Id test started");
            var getBookingByIdreq = new RestRequest("booking/" + bookinId, Method.Get);
            getBookingByIdreq.AddHeader("Content-Type", "application/json")
                   .AddHeader("Accept", "application/json");
            var response = client.Execute(getBookingByIdreq);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");

                var bookingDetailsResponse = JsonConvert.DeserializeObject<BookingDetails>(response.Content);
                Dates? dates = bookingDetailsResponse?.BookingDates;


                Assert.IsNotEmpty(bookingDetailsResponse?.FirstName);
                Log.Information("FirstName Returned");
                Assert.IsNotEmpty(bookingDetailsResponse?.LastName);
                Log.Information("LastName Returned");
                Assert.NotNull(bookingDetailsResponse?.TotalPrice);
                Log.Information("Total Price Returned");
                Assert.NotNull(bookingDetailsResponse.DepositPaid);
                Log.Information("Deposit paid Returned");

                Assert.IsNotEmpty(dates?.CheckIn);
                Log.Information("CheckIn details Returned");
                Assert.NotNull(dates?.CheckOut);
                Log.Information("Checkout details Returned");

                Log.Information("Get single user test passed all asserts");
                test.Pass("Get single user test passed all asserts");

            }
            catch (AssertionException)
            {
                test.Fail("Get singleUser test failed");
            }
        }
        [Test]
        [Order(1)]

        public void GetAllBooking()
        {
            test = extent.CreateTest("Get all user");
            Log.Information("Get all user test started");
            var req = new RestRequest("booking", Method.Get);
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                List<BookingData> book = JsonConvert.DeserializeObject<List<BookingData>>(response.Content);
                Assert.NotNull(book);
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
            var request = new RestRequest("booking", Method.Post);

            request.AddHeader("Content-Type", "application/json")
                   .AddHeader("Accept", "application/json");
            request.AddJsonBody(new
            {
                firstname = "Adithyan",
                lastname = "Engineer",
                totalprice = 101,
                depositpaid = true,
                bookingdates = new
                { checkin = "2018-01-01", checkout = "2018-02-01" },
                additionalneeds = "Breakfast"
            });

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                var bookingdataresponse = JsonConvert.DeserializeObject<BookingData>(response.Content);
                Assert.NotNull(bookingdataresponse?.BookingId);
                var bdetails = bookingdataresponse?.Booking;
                Assert.That(bdetails?.FirstName, Is.EqualTo("Adithyan"));
                Log.Information("First Name matches with fetch");
                Assert.That(bdetails?.LastName, Is.EqualTo("Engineer"));
                Log.Information("Last Name matches with fetch");
                Log.Information("Create users test passed all asserts");
                test.Pass("Create users test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Get all users test failed");
            }

        }
        [Test]
        [TestCase(15)]
        [Order(3)]
        public void UpdateBooking(int bookingId)
        {
            var token = CreateToken();
            test = extent.CreateTest("Update user");
            var updateRequest = new RestRequest("booking/" + bookingId, Method.Put);
            updateRequest.AddHeader("Cookie", "token=" + token).AddHeader("Accept", "application/json");

            updateRequest.AddHeader("Content-Type", "application/json");
            updateRequest.AddJsonBody(new
            {
                firstname = "Adithyan",
                lastname = "Engineer",
                totalprice = 101,
                depositpaid = true,
                bookingdates = new
                { checkin = "2018-01-01", checkout = "2018-02-01" },
                additionalneeds = "Breakfast"
            });
            var response = client.Execute(updateRequest);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API response: {response.Content}");
                var bookingDetailsResponse = JsonConvert.DeserializeObject<BookingDetails>(response.Content);
                Assert.That(bookingDetailsResponse?.FirstName, Is.EqualTo("Adithyan"));
                Log.Information("First Name matches with fetch");
                Assert.That(bookingDetailsResponse?.LastName, Is.EqualTo("Engineer"));
                Log.Information("Last Name matches with fetch");
                Log.Information("Update Booking test passed all asserts");
                test.Pass("UpdateBooking test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Update booking test failed");
            }

        }
        [Test]
        [TestCase(19)]
        [Order(4)]
        public void DeleteBooking(int userId)
        {
            test = extent.CreateTest("Delete Booking");
            var token = CreateToken();
            var deleterequest = new RestRequest("booking/" + userId, Method.Delete);
            deleterequest.AddHeader("Cookie", "token=" + token);
            var response = client.Execute(deleterequest);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API response: {response.Content}");
                Assert.That(response.Content, Is.EqualTo("Created"));
                Log.Information("Empty response returned");
                Log.Information("Delete users test passed all asserts");
                test.Pass("Delete users test passed all asserts");
            }
            catch (AssertionException)
            {
                test.Fail("Delete users test failed");
            }
        }

        public string CreateToken()
        {

            var CreateTokenrequest = new RestRequest("auth", Method.Post);

            CreateTokenrequest.AddHeader("Content-Type", "application/json");
            CreateTokenrequest.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(CreateTokenrequest);


            var getToken = JsonConvert.DeserializeObject<Authentication>(response.Content);

            return getToken.Token;
        }
    }
}