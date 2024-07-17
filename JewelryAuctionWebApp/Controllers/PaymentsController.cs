using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace JewelryAuctionWebApp.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly string apiUrl = "https://localhost:7169/api/Payment/";
        private readonly IHttpClientFactory _clientFactory;

        public PaymentsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search = "", string status = "", string paymentMethod = "", float? minPrice = null, float? maxPrice = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = new List<Payment>();
                using (var httpClient = new HttpClient())
                {
                    // Build the URL with query parameters for filtering and paging
                    var apiUrlWithParams = $"{apiUrl}GetAll?search={search}&pageNumber={pageNumber}&pageSize={pageSize}";

                    if (!string.IsNullOrEmpty(status))
                    {
                        apiUrlWithParams += $"&status={status}";
                    }

                    if (!string.IsNullOrEmpty(paymentMethod))
                    {
                        apiUrlWithParams += $"&paymentMethod={paymentMethod}";
                    }

                    if (minPrice != null)
                    {
                        apiUrlWithParams += $"&minPrice={minPrice}";
                    }
                        
                    if (maxPrice != null)
                    {
                        apiUrlWithParams += $"&maxPrice={maxPrice}";
                    }

                    using (var response = await httpClient.GetAsync(apiUrlWithParams))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Payment>>(content);
                        }
                    }
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Payment newPayment)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(newPayment), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "CreatePayment", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Payment created successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to create payment." });
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
        public async Task<IActionResult> Update([FromBody] Payment updatedPayment)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(updatedPayment), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync(apiUrl + "UpdatePayment", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 1, message = "Payment updated successfully." });
                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            return Json(new { status = 0, message = "Failed to update payment." });
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
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(apiUrl + "GetById?id=" + paymentId);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var payment = JsonConvert.DeserializeObject<Payment>(content);
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
        public async Task<IActionResult> Delete(int paymentId)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync(apiUrl + "DeletePayment?paymentId=" + paymentId);
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { status = 1, message = "Payment deleted successfully." });
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

        [HttpGet]
        public async Task<IActionResult> Filter(int? price, DateTime? date, int? jewelryId)
        {
            try
            {
                var queryParams = new List<string>();
                if (price.HasValue)
                    queryParams.Add($"price={price.Value}");
                if (date.HasValue)
                    queryParams.Add($"date={date.Value}");
                if (jewelryId.HasValue)
                    queryParams.Add($"jewelryId={jewelryId.Value}");

                var queryString = string.Join("&", queryParams);
                var apiUrlWithQuery = apiUrl + "Filter";
                if (!string.IsNullOrEmpty(queryString))
                    apiUrlWithQuery += "?" + queryString;

                var result = new List<Payment>();
                using (var httpClient = _clientFactory.CreateClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrlWithQuery))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Payment>>(content);
                        }
                        else
                        {
                            throw new HttpRequestException($"Failed to retrieve filtered data: {response.ReasonPhrase}");
                        }
                    }
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error filtering payments: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            try
            {
                var result = new List<Payment>();
                using (var httpClient = _clientFactory.CreateClient())
                {
                    using (var response = await httpClient.GetAsync(apiUrl + "Search?search=" + search))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            result = JsonConvert.DeserializeObject<List<Payment>>(content);
                        }
                        else
                        {
                            throw new HttpRequestException($"Failed to retrieve search results: {response.ReasonPhrase}");
                        }
                    }
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error searching payments: {ex.Message}");
            }
        }
    }
}
