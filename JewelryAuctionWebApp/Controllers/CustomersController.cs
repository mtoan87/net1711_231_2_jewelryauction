using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using JewelryAuctionBusiness;
using Newtonsoft.Json;
using JewelryAuctionData.DTO;
using System.Text;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using JewelryAuctionData.DTO.Customer;
using System.Net.Http;
using JewelryAuctionData.Paginate;
using Firebase.Storage;

namespace JewelryAuctionWebApp.Controllers
{
    public class CustomersController : Controller
    {

        private static string apiUrl = "https://localhost:7169/api/Customer/";
        private readonly IHttpClientFactory _httpClientFactory;



        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var pagedResult = await GetPaged(pageNumber, pageSize);
            return View(pagedResult);
            //return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(LoginDTO loginData)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(loginData);
        //    }
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.PostAsJsonAsync(apiUrl+ "Login", loginData);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var token = await response.Content.ReadAsStringAsync();

                
        //        HttpContext.Session.SetString("JWToken", token);

        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Invalid login attempt.";
        //        return View(loginData);
        //    }
        //}

        [HttpGet]
        public async Task<List<Customer>> GetAll()
        {
            try
            {
                var result = new List<Customer>();
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Customer>>(content);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<PagedResult<Customer>> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = new PagedResult<Customer>();
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync($"{apiUrl}GetPaged?pageNumber={pageNumber}&pageSize={pageSize}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<PagedResult<Customer>>(content);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var response = await httpClient.GetAsync(apiUrl + "Search?search=" + search);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var customers = JsonConvert.DeserializeObject<List<Customer>>(content);
                        return Json(customers);
                    }
                    else
                    {
                        return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer newPayment)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(newPayment), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "CreateCustomer", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Customer created successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to create customer." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Customer updatedPayment)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())

                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedPayment), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "UpdateCustomer", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Customer updated successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to update customer." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + customerId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var payment = JsonConvert.DeserializeObject<Customer>(content);
                        return Json(payment);
                    }
                    else
                    {
                        return Json(null);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int customerId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + customerId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var customer = JsonConvert.DeserializeObject<Customer>(content);
                        return View("Details", customer);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int customerId)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var response = await httpClient.DeleteAsync(apiUrl + "DeleteCustomer?customerId=" + customerId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Customer deleted successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to delete customers." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }
    }
}
