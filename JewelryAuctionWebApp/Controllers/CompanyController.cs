using JewelryAuctionData.Models;
using JewelryAuctionData.Paginate;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace JewelryAuctionWebApp.Controllers
{
    public class CompanyController : Controller
    {
        private string apiUrl = "https://localhost:7169/api/Company/";
        private readonly IHttpClientFactory _httpClientFactory;
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 3)
        {
            var pagedResult = await GetPaged(pageNumber, pageSize);
            return View(pagedResult);
            //return View();
        }
        public CompanyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<List<Company>> GetAll()
        {
            try
            {
                var result = new List<Company>();
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    var response = await httpClient.GetAsync(apiUrl + "GetAll");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<Company>>(content);
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
        public async Task<PagedResult<Company>> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var result = new PagedResult<Company>();
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
                            result = JsonConvert.DeserializeObject<PagedResult<Company>>(content);
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
        public async Task<IActionResult> Details(int companyId)
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

                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + companyId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var company = JsonConvert.DeserializeObject<Company>(content);
                        return View("Details", company);
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
                        var company = JsonConvert.DeserializeObject<List<Company>>(content);
                        return Json(company);
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
        public async Task<IActionResult> Create([FromBody] Company newCompany)
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
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(newCompany), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiUrl + "CreateCompany", jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Company created successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to create company." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Company updatedCompany)
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
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedCompany), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(apiUrl + "UpdateCompany", jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Company updated successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to update company." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyById(int companyId)
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
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + companyId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var company = JsonConvert.DeserializeObject<Company>(content);
                        return Json(company);
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
        public async Task<IActionResult> Delete(int companyId)
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
                    var response = await httpClient.DeleteAsync(apiUrl + "DeleteCompany?companyId=" + companyId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Company deleted successfully." });
                    }
                    else
                    {
                        return Json(new { status = 0, message = "Failed to delete company." });
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
