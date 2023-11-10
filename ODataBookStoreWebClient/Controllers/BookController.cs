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
using Microsoft.AspNetCore.Http;

namespace ODataBookStoreWebClient.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient httpClient = null;
        private string BookApiUrl = "";

        public BookController()
        {
            httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:44374/odata/Book"; // Điều chỉnh URL API của bạn
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await httpClient.GetAsync(BookApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookRequest> list = new List<BookRequest>();
            BookRequest obj = new BookRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Book>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new BookRequest(
               item.BookId,
               item.Title,
               item.Type,
               item.PubId,
               item.Advance,
               item.Price,
               item.Royalty,
               item.YtdSales,
               item.Notes,
               item.PublishedDate
                    );
                list.Add(obj);
            }



            return View(list);
        }
        public IActionResult Create()
        {
            return RedirectToAction("Index", "Book");
        }



        [HttpPost]
        public async Task<IActionResult> Create(String Tittle, String Type, String Price, String PublishedDate)
        {
            DateTime date = DateTime.Parse(PublishedDate);
            Debug.WriteLine(date);
            BookRequest person = new BookRequest(
                 1,
               Tittle,
               Type,
               1,
               "a",
               Decimal.Parse(Price),
               "a",
                "a",
                "a",
              date
                );
            Debug.WriteLine(person.PublishedDate);
            var json = JsonSerializer.Serialize(person);
            Debug.WriteLine(json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(BookApiUrl, content);


            return RedirectToAction("Index", "Book");
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"{BookApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Book");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> SearchID(int id, String a, String b,int c,String d)
        {
            String search = "?$filter=";
            if (id > 0)
            {
                search = search + "BookId eq " + id + " and ";
            }
            else if (id == 0)
            {
                search = search + "BookId ne " + 0 + " and ";
            }
            if (a is null)
            {
                search = search + "Title ne " + "''" + " and ";
            }
            else if (a is not null)
            {
                search = search + "Title eq " + "\'" + a + "\'" + " and ";
            }
            if (c == 0 )
            {
                search = search + "Price ne " + 0;
            }
            else if (c > 0)
            {
                search = search + "Price eq " +  c;
            }
            Debug.WriteLine(search);
            HttpResponseMessage response = await httpClient.GetAsync($"{BookApiUrl}{search}");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<BookRequest> list = new List<BookRequest>();
            BookRequest obj = new BookRequest();
            var oDataResponse = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OData<List<Book>>>(strData);


            foreach (var item in oDataResponse.value)
            {
                obj = new BookRequest(
               item.BookId,
               item.Title,
               item.Type,
               item.PubId,
               item.Advance,
               item.Price,
               item.Royalty,
               item.YtdSales,
               item.Notes,
               item.PublishedDate
                    );
                list.Add(obj);
            }

            return View(list);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(DataTransfer.BookRequest memberRequest)
        {
 
            try
            {

                // Serialize the updated productRequest to JSON
                var json = JsonSerializer.Serialize(memberRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Make a PUT request to update the product
                HttpResponseMessage response = await httpClient.PutAsync($"{BookApiUrl}/{memberRequest.BookId}", content);
                Debug.WriteLine(response.IsSuccessStatusCode);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index", "Book");
                }


                return RedirectToAction("Index", "Book");
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
                HttpResponseMessage response = await httpClient.GetAsync($"{BookApiUrl}?$filter=BookId eq " + id);

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    List<BookRequest> list = new List<BookRequest>();
                    BookRequest obj = new BookRequest();
                    var oDataResponse = Newtonsoft.Json.JsonConvert
                        .DeserializeObject<OData<List<Book>>>(strData);


                    foreach (var item in oDataResponse.value)
                    {
                        obj = new BookRequest(
                       item.BookId,
                       item.Title,
                       item.Type,
                       item.PubId,
                       item.Advance,
                       item.Price,
                       item.Royalty,
                       item.YtdSales,
                       item.Notes,
                       item.PublishedDate
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
