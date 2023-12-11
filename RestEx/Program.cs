using Newtonsoft.Json.Linq;
using RestSharp;

//string baseUrl = "https://reqres.in/api/";
//var client = new RestClient(baseUrl);

////GET Method
//var getUserRequest = new RestRequest("user/2", Method.Get);
//var getUserResponse = client.Execute(getUserRequest);
//Console.WriteLine("GET response: \n"+getUserResponse.Content);


////POST Method
//var createUserRequest = new RestRequest("users",Method.Post);
//createUserRequest.AddParameter("name", "Adithyan");
//createUserRequest.AddParameter("job", "Rocket expert");

//var createUserResponse = client.Execute(createUserRequest);
//Console.WriteLine("Post Create User Response:\n"+createUserResponse.Content);

////Update Method

//var updateUserRequest = new RestRequest("users/2", Method.Put);
//updateUserRequest.AddParameter("name", "Rocket Adithyan");
//updateUserRequest.AddParameter("job", "Super rocket expert");

//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("PUT Update User Response: \n"+updateUserResponse.Content);

////Delete Method
//var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//var deleteUserResponse = client.Execute(deleteUserRequest);
//Console.WriteLine("Delete User Response:\n" + deleteUserResponse.Content);

//string baseUrl = "https://reqres.in/api/";
//var client = new RestClient(baseUrl);

////GET Method
//var getUserRequest = new RestRequest("users/2", Method.Get);
////getUserRequest.AddQueryParameter("page", "2"); //Adding query parameter
//var getUserResponse = client.Execute(getUserRequest);
//if(getUserResponse.StatusCode ==System.Net.HttpStatusCode.OK)
//{
//    //Parse Json response content
//    JObject? userJson = JObject.Parse(getUserResponse?.Content);
//   // string? pageno = userJson["page"]?.ToString();
//    string? userName = userJson["data"]?["first_name"]?.ToString();
//    string? userLastName = userJson["data"]?["last_name"]?.ToString();
//    Console.WriteLine($"User Name : {userName} {userLastName}");
//}
//else
//{
//    Console.WriteLine($"Error:{getUserResponse.ErrorMessage}");
//}


////POST Method
//var createUserRequest = new RestRequest("users", Method.Post);
//createUserRequest.AddHeader("Content-Type", "application/JSON");
//createUserRequest.AddJsonBody(new {name = "Adithyan",job ="Rocket developer"});

//var createUserResponse = client.Execute(createUserRequest);
//Console.WriteLine("Post Create User Response:\n" + createUserResponse.Content);

////Update Method

//var updateUserRequest = new RestRequest("users/2", Method.Put);
////updateUserRequest.AddQueryParameter("page", "2");  //NO QUESTION MARK

//updateUserRequest.AddHeader("Content-Type", "application/JSON");
//updateUserRequest.AddJsonBody(new { name = "Rocket-Adithyan", job = "Super Rocket developer" });

//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("PUT Update User Response: \n" + updateUserResponse.Content);

////Delete Method
//var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//var deleteUserResponse = client.Execute(deleteUserRequest);
//Console.WriteLine("Delete User Response:\n" + deleteUserResponse.Content);

string baseUrl = "https://reqres.in/api/";
var client = new RestClient(baseUrl);
GetAll(client);
AddUser(client);
UpdateUser(client);
DeletUser(client);





    

//GET Method
static void GetAll(RestClient client)
{
    var getUserRequest = new RestRequest("user/2", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine("GET response: \n" + getUserResponse.Content);
}

//POST Method
static void  AddUser(RestClient client)
{
    var createUserRequest = new RestRequest("users", Method.Post);
    createUserRequest.AddParameter("name", "Adithyan");
    createUserRequest.AddParameter("job", "Rocket expert");

    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("Post Create User Response:\n" + createUserResponse.Content);
}
//Update Method

static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("users/2", Method.Put);
    updateUserRequest.AddParameter("name", "Rocket Adithyan");
    updateUserRequest.AddParameter("job", "Super rocket expert");

    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT Update User Response: \n" + updateUserResponse.Content);
}


//Delete Method
static void DeletUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("users/2", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("Delete User Response:\n" + deleteUserResponse.Content);
}