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
using JewelryAuctionBusiness;
using JewelryAuctionData.Paginate;
using System.Net.Http.Headers;

namespace JewelryAuctionWebApp.Controllers
{
    public class JoinAuctionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string apiUrl = "https://localhost:7169/api/JoinAuction/";

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var pagedResult = await GetPaged(pageNumber, pageSize);
            return View(pagedResult);
            //return View();
        }

        public JoinAuctionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //[HttpGet]
        //public async Task<List<JoinAuction>> GetAll()
        //{
        //    try
        //    {
        //        var result = new List<JoinAuction>();
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
        //            {
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var content = await response.Content.ReadAsStringAsync();
        //                    result = JsonConvert.DeserializeObject<List<JoinAuction>>(content);
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
        public async Task<PagedResult<JoinAuction>> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = new PagedResult<JoinAuction>();
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
                            result = JsonConvert.DeserializeObject<PagedResult<JoinAuction>>(content);
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
        public async Task<IActionResult> GetJoinAuctionById(int joinAuctionId)
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

                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + joinAuctionId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var joinAuction = JsonConvert.DeserializeObject<JoinAuction>(content);
                        return Json(joinAuction);
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
                        var joinAuction = JsonConvert.DeserializeObject<List<JoinAuction>>(content);
                        return Json(joinAuction);
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
        public async Task<IActionResult> FilterJoinAuctions(int? customerId, DateTime? startTime, DateTime? endTime)
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
                    if (customerId.HasValue) queryParams.Add($"customerId={customerId.Value}");
                    if (startTime.HasValue) queryParams.Add($"startTime={startTime.Value.ToString("o")}");
                    if (endTime.HasValue) queryParams.Add($"endTime={endTime.Value.ToString("o")}");

                    var queryString = string.Join("&", queryParams);

                    var response = await httpClient.GetAsync(apiUrl + "Filter?" + queryString);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var joinAuctions = JsonConvert.DeserializeObject<List<JoinAuction>>(content);
                        return Json(joinAuctions);
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
        public async Task<IActionResult> Create([FromBody] JoinAuction joinAuction)
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

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(joinAuction), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "Create", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Auction created successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to create auction." });
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
        public async Task<IActionResult> Update([FromBody] JoinAuction joinAuction)
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

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(joinAuction), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "Update", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Auction updated successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to update auction." });
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
        public async Task<IActionResult> Delete(int joinAuctionId)
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

                    var response = await httpClient.DeleteAsync(apiUrl + "Delete?joinAuctionId=" + joinAuctionId);
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
