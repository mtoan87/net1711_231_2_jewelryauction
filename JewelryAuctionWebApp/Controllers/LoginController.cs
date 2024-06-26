using JewelryAuctionData.DTO.Customer;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginData)
        {
            if (!ModelState.IsValid)
            {
                return View(loginData);
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7169/api/Customer/Login", loginData);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();

                
                HttpContext.Session.SetString("JWToken", token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid login attempt.";
                return View(loginData);
            }
        }
    }
}
