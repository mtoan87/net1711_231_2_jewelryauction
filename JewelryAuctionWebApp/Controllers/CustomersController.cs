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

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("add", new Customer());
        }


    }
}
