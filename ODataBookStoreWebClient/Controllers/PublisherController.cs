using BusinessObjects.Models;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using ODataBookStoreWebClient.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System;

namespace ODataBookStoreWebClient.Controllers
{
    public class PublisherController : Controller
    {
        private readonly HttpClient httpClient = null;
        private string BookApiUrl = "";

        public PublisherController()
        {
            httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:44374/odata/Publisher"; // Điều chỉnh URL API của bạn
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync(BookApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<PublisherRequest> list = new List<PublisherRequest>();
            PublisherRequest obj = new PublisherRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Publisher>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new PublisherRequest(
               item.PubId,
                 item.PublisherName,
               item.City,
               item.State,
              item.Country
              
                    );
                list.Add(obj);
            }



            return View(list);
        }
        public IActionResult Create()
        {
            return RedirectToAction("Index", "Publisher");
        }



        [HttpPost]
        public async Task<IActionResult> Create(String FirstName, String City)
        {
      
            PublisherRequest person = new PublisherRequest(
                 1,
               FirstName,
               City,
               "",
               ""
                );
            var json = JsonSerializer.Serialize(person);
            Debug.WriteLine(json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(BookApiUrl, content);


            return RedirectToAction("Index", "Publisher");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"{BookApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Publisher");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> SearchID(int id,String name, String city)
        {
            Debug.WriteLine(id + "," + name + "," + city); 
            String search = "?$filter=";
            if(id > 0)
            {
                search = search + "PubId eq " + id + " and ";
            }
            else if (id == 0)
            {
                search = search + "PubId ne " + 0 + " and ";
            }
            if (name is null)
            {
                search = search + "PublisherName ne " + "''" + " and ";
            }
            else if (name is not null)
            {
                search = search + "PublisherName eq " + "\'"+name+"\'" + " and ";
            }
            if (city is null)
            {
                search = search + "City ne " + "''";
            }
            else if (city is not null)
            {
                search = search + "City eq " + "\'" + city + "\'";
            }



            Debug.WriteLine(id);
            Debug.WriteLine(name);
            Debug.WriteLine(city);
            Debug.WriteLine(search);
            HttpResponseMessage response = await httpClient.GetAsync($"{BookApiUrl}{search}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<PublisherRequest> list = new List<PublisherRequest>();
            PublisherRequest obj = new PublisherRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Publisher>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new PublisherRequest(
               item.PubId,
                 item.PublisherName,
               item.City,
               item.State,
              item.Country
                    );
                list.Add(obj);
            }
            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(DataTransfer.PublisherRequest memberRequest)
        {

            try
            {

                // Serialize the updated productRequest to JSON
                var json = JsonSerializer.Serialize(memberRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Make a PUT request to update the product
                HttpResponseMessage response = await httpClient.PutAsync($"{BookApiUrl}/{memberRequest.PubId}", content);
                Debug.WriteLine(response.IsSuccessStatusCode);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index", "Publisher");
                }


                return RedirectToAction("Index", "Publisher");
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
                HttpResponseMessage response = await httpClient.GetAsync($"{BookApiUrl}?$filter=PubId eq " + id);

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    List<PublisherRequest> list = new List<PublisherRequest>();
                    PublisherRequest obj = new PublisherRequest();
                    var oDataResponse = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<OData<List<Publisher>>>(strData);


                    foreach (var item in oDataResponse.value)
                    {
                        obj = new PublisherRequest(
                       item.PubId,
                         item.PublisherName,
                       item.City,
                       item.State,
                      item.Country

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
