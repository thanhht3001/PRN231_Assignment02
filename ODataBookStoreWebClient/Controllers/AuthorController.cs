using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using DataTransfer;
using BusinessObjects.Models;
using System.Net.Mail;
using System.Net;
using ODataBookStoreWebClient.Models;
using System;

namespace ODataBookStoreWebClient.Controllers
{
    public class AuthorController : Controller
    {
        private readonly HttpClient httpClient = null;
        private string AuthorApiUrl = "";
        public AuthorController()
        {
            httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = "https://localhost:44374/odata/Author"; // Điều chỉnh URL API của bạn
        }
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync(AuthorApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<AuthorRequest> list = new List<AuthorRequest>();
            AuthorRequest obj = new AuthorRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Author>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new AuthorRequest(
               item.AuthorId,
               item.EmailAddress,
               item.Address,
               item.LastName,
               item.FistName,
               item.Phone,
               item.City,
               item.Zip
                    );
                list.Add(obj);
            }



            return View(list);
        }
        public IActionResult Create()
        {
            return RedirectToAction("Index","Author");
        }



        [HttpPost]
        public async Task<IActionResult> Create(String FirstName, String LastName, String City, String EmailAddress)
        {

                AuthorRequest person = new AuthorRequest(1, EmailAddress,"", LastName, FirstName,"", City, "123"); 
                
                var json = JsonSerializer.Serialize(person);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(AuthorApiUrl, content);
           

            return RedirectToAction("Index", "Author");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"{AuthorApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Author");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> SearchID(int id, String a, String b,String c)
        {
            Debug.WriteLine(id + "," + a + "," + b + "," + c);
            String search = "?$filter=";
            if (id > 0)
            {
                search = search + "AuthorId eq " + id + " and ";
            }
            else if (id == 0)
            {
                search = search + "AuthorId ne " + 0 + " and ";
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
                search = search + "LastName ne " + "''" + " and ";
            }
            else if (b is not null)
            {
                search = search + "LastName eq " + "\'" + b + "\'" + " and ";
            }
            if (c is null)
            {
                search = search + "City ne " + "''";
            }
            else if (c is not null)
            {
                search = search + "City eq " + "\'" + b + "\'";
            }
            Debug.WriteLine(search);
            HttpResponseMessage response = await httpClient.GetAsync($"{AuthorApiUrl}{search}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<AuthorRequest> list = new List<AuthorRequest>();
            AuthorRequest obj = new AuthorRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Author>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new AuthorRequest(
               item.AuthorId,
               item.EmailAddress,
               item.Address,
               item.LastName,
               item.FistName,
               item.Phone,
               item.City,
               item.Zip
                    );
                list.Add(obj);
            }
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DataTransfer.AuthorRequest memberRequest)
        {
            Debug.WriteLine(memberRequest.AuthorId);
            Debug.WriteLine(memberRequest.City);
            Debug.WriteLine(memberRequest.Zip);
            Debug.WriteLine(memberRequest.Address);
            Debug.WriteLine(memberRequest.EmailAddress);
            Debug.WriteLine(memberRequest.FistName);
            Debug.WriteLine(memberRequest.LastName);
            Debug.WriteLine(memberRequest.Phone);
            try
            {
               
                    // Serialize the updated productRequest to JSON
                    var json = JsonSerializer.Serialize(memberRequest);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    // Make a PUT request to update the product
                    HttpResponseMessage response = await httpClient.PutAsync($"{AuthorApiUrl}/{memberRequest.AuthorId}", content);
                    Debug.WriteLine(response.IsSuccessStatusCode);

                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index", "Author");
                }

                
                return RedirectToAction("Index", "Author");
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
                HttpResponseMessage response = await httpClient.GetAsync($"{AuthorApiUrl}?$filter=AuthorId eq " + id);

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    List<AuthorRequest> list = new List<AuthorRequest>();
                    AuthorRequest obj = new AuthorRequest();
                    var oDataResponse = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<OData<List<Author>>>(strData);


                    foreach (var item in oDataResponse.value)
                    {
                        obj = new AuthorRequest(
                       item.AuthorId,
                       item.EmailAddress,
                       item.Address,
                       item.LastName,
                       item.FistName,
                       item.Phone,
                       item.City,
                       item.Zip
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


