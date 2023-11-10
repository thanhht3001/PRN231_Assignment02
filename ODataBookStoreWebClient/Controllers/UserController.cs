using BusinessObjects.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using ODataBookStoreWebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace ODataBookStoreWebClient.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient httpClient = null;
        private string UserApiUrl = "";

        public UserController()
        {
            httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = "https://localhost:44374/odata/User"; // Điều chỉnh URL API của bạn
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync(UserApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<UserRequest> list = new List<UserRequest>();
            UserRequest obj = new UserRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<User>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new UserRequest(
               item.UserId,
               item.EmailAddress,
               item.Source,
               item.FistName,
               item.MiddleName,
               item.LastName,
               item.HireDate,
               item.RoleId,
               item.PubId,
               item.Password
                    );
                list.Add(obj);
            }

            return View(list);
        }
        public IActionResult Create()
        {
            return RedirectToAction("Index", "User");
        }



        [HttpPost]
        public async Task<IActionResult> Create(String FirstName, String MiddleName, String LastName, String Source, String EmailAddress)
        {
      
            UserRequest person = new UserRequest(
                1,
              EmailAddress,
              Source,
               FirstName,
              MiddleName,
             LastName,
              DateTime.Now,
              1,
              1,
              "123456"
                );
            var json = JsonSerializer.Serialize(person);
            Debug.WriteLine(json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(UserApiUrl, content);


            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"{UserApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> SearchID(int id, String a, String b)
        {
            String search = "?$filter=";
            if (id > 0)
            {
                search = search + "UserId eq " + id + " and ";
            }
            else if (id == 0)
            {
                search = search + "UserId ne " + 0 + " and ";
            }
            if (a is null)
            {
                search = search + "FistName ne " + "''" + " and ";
            }
            else if (a is not null)
            {
                search = search + "FistName eq " + "\'" + a + "\'" + " and ";
            }
            if (b is null)
            {
                search = search + "EmailAddress ne " + "''" ;
            }
            else if (b is not null)
            {
                search = search + "EmailAddress eq " + "\'" + b + "\'" ;
            }

            Debug.WriteLine(search);
            HttpResponseMessage response = await httpClient.GetAsync($"{UserApiUrl}{search}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<UserRequest> list = new List<UserRequest>();
            UserRequest obj = new UserRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<User>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new UserRequest(
               item.UserId,
               item.EmailAddress,
               item.Source,
               item.FistName,
               item.MiddleName,
               item.LastName,
               item.HireDate,
               item.RoleId,
               item.PubId,
               item.Password
                    );
                list.Add(obj);
            }
            return View(list);
        }


        public async Task<IActionResult> Profile(int myObject)
        {
            Debug.WriteLine(myObject);
            try
            {
                // Make a GET request to the API to retrieve the product data
                HttpResponseMessage response = await httpClient.GetAsync($"{UserApiUrl}?$filter=UserId eq "+ myObject);

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    List<UserRequest> list = new List<UserRequest>();
                    UserRequest obj = new UserRequest();
                    var oDataResponse = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<OData<List<User>>>(strData);


                    foreach (var item in oDataResponse.value)
                    {
                        obj = new UserRequest(
                       item.UserId,
                       item.EmailAddress,
                       item.Source,
                       item.FistName,
                       item.MiddleName,
                       item.LastName,
                       item.HireDate,
                       item.RoleId,
                       item.PubId,
                       item.Password
                            );
                        list.Add(obj);
                    }

                    // Pass the productRequest to the view for editing
                    return View(list[0]);
                }
                else
                {
                    // Handle the case where the API request was not successful
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error) and return an error view
                ModelState.AddModelError(string.Empty, "Error loading product for editing.");
                return View();
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditProfile(DataTransfer.UserRequest memberRequest)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    // Serialize the updated productRequest to JSON
                    var json = JsonSerializer.Serialize(memberRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    // Make a PUT request to update the product
                    HttpResponseMessage response = await httpClient.PutAsync($"{UserApiUrl}/{memberRequest.UserId}", content);
                    Debug.WriteLine(response.IsSuccessStatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        int myObject = (memberRequest.UserId); // Đối tượng cần truyền
                        var routeValues = new RouteValueDictionary
    {
              { "myObject", myObject }
    };
                        // Redirect to the Index action upon successful update
                        return RedirectToAction("Profile","User", routeValues);
                    }
                    else
                    {
                        // Handle the case where the API request to update the product was not successful
                        ModelState.AddModelError(string.Empty, "Error updating product.");
                    }
                }

                // If ModelState is not valid or the API request fails, return to the edit view
                return RedirectToAction("Edit");
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error) and return to the edit view with an error message
                ModelState.AddModelError(string.Empty, "Error updating product.");
                return View(memberRequest);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
      
            try
            {
                // Make a GET request to the API to retrieve the product data
                HttpResponseMessage response = await httpClient.GetAsync($"{UserApiUrl}?$filter=UserId eq " + id);

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    List<UserRequest> list = new List<UserRequest>();
                    UserRequest obj = new UserRequest();
                    var oDataResponse = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<OData<List<User>>>(strData);


                    foreach (var item in oDataResponse.value)
                    {
                        obj = new UserRequest(
                       item.UserId,
                       item.EmailAddress,
                       item.Source,
                       item.FistName,
                       item.MiddleName,
                       item.LastName,
                       item.HireDate,
                       item.RoleId,
                       item.PubId,
                       item.Password
                            );
                        list.Add(obj);
                    }

                    // Pass the productRequest to the view for editing
                    return View(list[0]);
                }
                else
                {
                    // Handle the case where the API request was not successful
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error) and return an error view
                ModelState.AddModelError(string.Empty, "Error loading product for editing.");
                return View();
            }
        }

    }
}
