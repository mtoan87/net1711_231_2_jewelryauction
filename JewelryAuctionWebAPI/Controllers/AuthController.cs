using JewelryAuctionWebAPI.Contracts.Login;
using JewelryAuctionWebAPI.JwtServices;
using JewelryAuctionWebAPI.JwtServices.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtGeneratorTokenService _jwtGeneratorTokenService;

        public AuthController(IAuthService authService, IJwtGeneratorTokenService jwtGeneratorTokenService)
        {
            _authService = authService;
            _jwtGeneratorTokenService = jwtGeneratorTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var customer = await _authService.Login(loginRequest);
            if (customer == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = _jwtGeneratorTokenService.GenerateToken(customer);

            var response = new LoginResponse
            {
                Customer = customer,
                Token = token
            };

            return Ok(response);
        }
    }
}
