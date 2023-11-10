using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using ODataBookStoreWebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ODataBookStoreWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client;
        private string MemberApiUrl = "";
        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:44374/odata/User"; // Điều chỉnh URL API của bạn
        }
        public async Task<IActionResult> Login(String Email, String Password)
        {
            string email = Email;
            string pass = Password;

            HttpResponseMessage response = await client.GetAsync(MemberApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<User> listMebers = Newtonsoft.Json.JsonConvert
           .DeserializeObject<OData<List<User>>>(strData).value;
            Debug.WriteLine(email + " " + pass);
            foreach (var item in listMebers)
            {
                if (item.EmailAddress.Equals(email))
                {
                    if (item.Password.Equals(pass))
                    {

                        if (item.RoleId == 1)
                        {
                            int myObject = (item.UserId); // Đối tượng cần truyền
                            var routeValues = new RouteValueDictionary
    {
              { "myObject", myObject }
    };
                            return RedirectToAction("Profile", "User", routeValues);
                        }
                        return RedirectToAction("Index", "Book");
                    }
                }
            }
            string myString = "Email or Password is not correct";
            TempData["MyString"] = myString;
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



    }
}
