using JewelryAuctionData.Models;
using JewelryAuctionData.Paginate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace JewelryAuctionWebApp.Controllers
{
    public class JewelryController : Controller
    {


        private readonly string apiUrl = "https://localhost:7169/api/Jewelry/";


        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var pagedResult = await GetPaged(pageNumber, pageSize);
            return View(pagedResult);
            //return View();
        }

        [HttpGet]
        public async Task<PagedResult<Jewelry>> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = new PagedResult<Jewelry>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiUrl}GetPaged?pageNumber={pageNumber}&pageSize={pageSize}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<PagedResult<Jewelry>>(content);
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

        /*[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = new List<Jewelry>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Jewelry>>(content);
                            Console.WriteLine(result);
                        }
                    }
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        [HttpGet]
        public async Task<IActionResult> Detail(int jewrlryId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + jewrlryId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var jewelry = JsonConvert.DeserializeObject<Jewelry>(content);
                        return View("Detail", jewelry);
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

        [HttpGet]
        public async Task<IActionResult> Search(string searchInput)

        {
            
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "Search?search=" + searchInput);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var bid = JsonConvert.DeserializeObject<List<Jewelry>>(content);
                        return Json(bid);
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
        public async Task<IActionResult> Create([FromBody] Jewelry newJewelry)
        {       

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(newJewelry), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "CreateJewelry", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {

                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Jewelry created successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to create Jewelry." });
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
        public async Task<IActionResult> Update([FromBody] Jewelry updatedJewelry)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedJewelry), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "UpdateJewelry", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Jewelry updated successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to update Jewelry." });
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
        public async Task<IActionResult> GetJewelryById(int JewelryId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + JewelryId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Jewelry = JsonConvert.DeserializeObject<Jewelry>(content);
                        return Json(Jewelry);
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
        public async Task<IActionResult> Delete(int JewelryId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(apiUrl + "DeleteJewelry?jewelryId=" + JewelryId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Jewelry deleted successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to delete Jewelry." });
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
