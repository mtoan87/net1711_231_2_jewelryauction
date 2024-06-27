using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JewelryAuctionWebApp.Controllers
{
    public class CompanyController : Controller
    {
        private string apiUrl = "https://localhost:7169/api/Company/";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Company>> GetAll()
        {
            try
            {
                var result = new List<Company>();
                using (var httpClient = new HttpClient())
                {
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
        public async Task<IActionResult> Search(string search)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
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
                using (var httpClient = new HttpClient())
                {
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
                using (var httpClient = new HttpClient())
                {
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
                using (var httpClient = new HttpClient())
                {
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
                using (var httpClient = new HttpClient())
                {
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
