using JewelryAuctionData.DTO;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JewelryAuctionWebApp.Controllers
{
    public class RequestAuctionDetailController : Controller
    {
        private string apiUrl = "https://localhost:7169/api/RequestAuctionDetail/";
        public RequestAuctionDetailController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<RequestAuctionDetail>> GetAll()
        {
            try
            {
                var result = new List<RequestAuctionDetail>();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrl + "GetAll"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<RequestAuctionDetail>>(content);
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
        public ActionResult Add()
        {
            return PartialView("add", new RequestAuctionDetail());
        }

        [HttpPost]
        public async Task<RequestAuctionDetail> Create([FromBody] RequestAuctionDetail auction)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(auction);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync($"{apiUrl}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var createdAuction = JsonConvert.DeserializeObject<RequestAuctionDetail>(responseContent);
                            return createdAuction;
                        }
                        else
                        {
                            throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync($"{apiUrl}{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<RequestAuctionDetail> Update(int id, [FromBody] UpdateRequestAuctionDetailDTO UpdateAuctionDTO)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(UpdateAuctionDTO);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync($"{apiUrl}{id}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var updatedAuction = JsonConvert.DeserializeObject<RequestAuctionDetail>(responseContent);
                            return updatedAuction;
                        }
                        else
                        {
                            throw new Exception($"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                RequestAuctionDetail result = null;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"{apiUrl}{id}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<RequestAuctionDetail>(content);
                        }
                    }
                }

                return PartialView("edit", result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
