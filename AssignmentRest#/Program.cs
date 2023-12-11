using Newtonsoft.Json.Linq;
using RestSharp;

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);


GetAllUser(client);
GetSingleUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);

//Get All

static void GetAllUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts", Method.Get);
    getUserRequest.AddQueryParameter("page", "1");
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("Get all Response: \n" + getUserResponse.Content);
}


//GET Method
static void GetSingleUser(RestClient client)
{


    var getUserRequest = new RestRequest("posts/1", Method.Get);
    //getUserRequest.AddQueryParameter("page", "2"); //Adding query parameter
    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    {
        //Parse Json response content
        JObject? userJson = JObject.Parse(getUserResponse?.Content);
        // string? pageno = userJson["page"]?.ToString();
        string? userId = userJson["data"]?["id"]?.ToString();
        string? userTitle = userJson["data"]?["title"]?.ToString();
        Console.WriteLine($"User Name : {userId} {userTitle}");
    }
    else
    {
        Console.WriteLine($"Error:{getUserResponse.ErrorMessage}");
    }
}


//POST Method
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/JSON");
    createUserRequest.AddJsonBody(new { userId = "44", id = "43", title = "Horse", body = "fun" });

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("Post Create User Response:\n" + createUserResponse.Content);
}
//Update Method

static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/1", Method.Put);
    //updateUserRequest.AddQueryParameter("page", "2");  //NO QUESTION MARK

    updateUserRequest.AddHeader("Content-Type", "application/JSON");
    updateUserRequest.AddJsonBody(new { userId = "44", id = "43", title = "lion", body = "fun" });

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update User Response: \n" + updateUserResponse.Content);
}
//Delete Method
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("Delete User Response:\n" + deleteUserResponse.Content);
}