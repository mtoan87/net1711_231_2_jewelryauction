using JewelryAuctionData.Models;
using JewelryAuctionData.Paginate;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JewelryAuctionWebApp.Controllers
{
    public class AuctionResultController : Controller
    {
        private string apiUrl = "https://localhost:7169/api/AuctionResult/";

        public AuctionResultController()
        {
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var pagedResult = await GetPaged(pageNumber, pageSize);
            return View(pagedResult);
            //return View();
        }

        [HttpGet]
        public async Task<PagedResult<AuctionResult>> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = new PagedResult<AuctionResult>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiUrl}GetPaged?pageNumber={pageNumber}&pageSize={pageSize}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<PagedResult<AuctionResult>>(content);
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
                var result = new List<AuctionResult>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<AuctionResult>>(content);
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
        public async Task<IActionResult> Details(int auctionResultId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + auctionResultId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var auctionResult = JsonConvert.DeserializeObject<AuctionResult>(content);
                        return View("Details", auctionResult);
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
        public async Task<IActionResult> Search(string searchInput1, string searchInput2, string searchInput3)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "Search?search1=" + searchInput1 + "&search2=" + searchInput2 + "&search3=" + searchInput3);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var bid = JsonConvert.DeserializeObject<List<AuctionResult>>(content);
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
        public async Task<IActionResult> Create([FromBody] AuctionResult newAuctionResult)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(newAuctionResult), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "CreateAuctionResult", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Auction Result created successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to create Auction Result." });
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
        public async Task<IActionResult> Update([FromBody] AuctionResult updatedAuctionResult)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedAuctionResult), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "UpdateAuctionResult", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Auction Result updated successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to update Auction Result." });
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
        public async Task<IActionResult> GetAuctionResultById(int AuctionResultId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + AuctionResultId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var AuctionResult = JsonConvert.DeserializeObject<AuctionResult>(content);
                        Console.WriteLine();
                        return Json(AuctionResult);
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
        public async Task<IActionResult> Delete(int AuctionResultId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(apiUrl + "DeleteAuctionResult?auctionResultId=" + AuctionResultId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Auction Result deleted successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to delete Auction Result." });
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
