using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JewelryAuctionData.Models;
using Newtonsoft.Json;
using System.Text;
using System.Security.Cryptography;
using JewelryAuctionData.Paginate;
using System.Net.Http.Headers;

namespace JewelryAuctionWebApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string apiUrl = "https://localhost:7169/api/Bid/";

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var pagedResult = await GetPaged(pageNumber, pageSize);
            return View(pagedResult);
            //return View();
        }

        public BidsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //[HttpGet]
        //public async Task<List<Bid>> GetAll()
        //{
        //    try
        //    {
        //        var result = new List<Bid>();
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var content = await response.Content.ReadAsStringAsync();
        //                    result = JsonConvert.DeserializeObject<List<Bid>>(content);
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [HttpGet]
        public async Task<PagedResult<Bid>> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = new PagedResult<Bid>();
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
                            result = JsonConvert.DeserializeObject<PagedResult<Bid>>(content);
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
        public async Task<IActionResult> GetBidById(int bidId)
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

                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + bidId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var bid = JsonConvert.DeserializeObject<Bid>(content);
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int bidId)
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

                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + bidId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var bid = JsonConvert.DeserializeObject<Bid>(content);
                        return View("Details", bid);
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
        public async Task<List<Bid>> GetBidsByJoinAuctionId(int joinAuctionId)
        {
            try
            {
                var result = new List<Bid>();
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }

                    using (var response = await httpClient.GetAsync(apiUrl + "GetBidsByJoinAuctionId?joinAuctionId=" + joinAuctionId))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Bid>>(content);
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
                        var bid = JsonConvert.DeserializeObject<List<Bid>>(content);
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

        [HttpGet]
        public async Task<IActionResult> FilterBids(double? bidAmount, DateTime? dateTime, int? jewelryId)
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

                    var queryParams = new List<string>();
                    if (bidAmount.HasValue) queryParams.Add($"bidAmount={bidAmount.Value}");
                    if (dateTime.HasValue) queryParams.Add($"dateTime={dateTime.Value.ToString("o")}");
                    if (jewelryId.HasValue) queryParams.Add($"jewelryId={jewelryId.Value}");

                    var queryString = string.Join("&", queryParams);

                    var response = await httpClient.GetAsync(apiUrl + "Filter?" + queryString);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var bids = JsonConvert.DeserializeObject<List<Bid>>(content);
                        return Json(bids);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Bid bid)
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

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(bid), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "Create", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Bid created successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to create Bid." });
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
        public async Task<IActionResult> Update([FromBody] Bid bid)
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

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(bid), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "Update", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Bid updated successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Fail to update Bid." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int bidId)
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

                    var response = await httpClient.DeleteAsync(apiUrl + "Delete?bidId=" + bidId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Deleted successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to delete." });
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
