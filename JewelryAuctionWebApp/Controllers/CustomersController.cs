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

namespace JewelryAuctionWebApp.Controllers
{
    public class CustomersController : Controller
    {

        private string apiUrl = "https://localhost:44357/api/Customer/";

        
        
        public CustomersController()
        {
           
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Customer>> GetAll()
        {
            try
            {
                var result = new List<Customer>();
                using (var httpClient = new HttpClient())
                {
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

       

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer newPayment)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
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
                using (var httpClient = new HttpClient())
                {
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
                using (var httpClient = new HttpClient())
                {
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

        [HttpDelete]
        public async Task<IActionResult> Delete(int customerId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(apiUrl + "DeleteCustomer?customerId=" + customerId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Customer deleted successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to delete payment." });
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
