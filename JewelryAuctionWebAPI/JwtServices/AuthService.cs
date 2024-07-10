using JewelryAuctionData.Models;
using JewelryAuctionWebAPI.Contracts.Login;
using JewelryAuctionWebAPI.JwtServices.IServices;
using Microsoft.EntityFrameworkCore;

namespace JewelryAuctionWebAPI.JwtServices
{
    public class AuthService : IAuthService
    {
        private readonly Net17112312JewelryAuctionContext _context;
        private readonly IJwtGeneratorTokenService _jwtGeneratorTokenService;

        public AuthService(Net17112312JewelryAuctionContext context, IJwtGeneratorTokenService jwtGeneratorTokenService)
        {
            _context = context;
            _jwtGeneratorTokenService = jwtGeneratorTokenService;
        }

        public async Task<Customer> Login(LoginRequest loginRequest)
        {
            var cus = await _context.Customers.SingleOrDefaultAsync(u => u.Email == loginRequest.Email && u.Password == loginRequest.Password);
            if (cus == null)
            {
                return null; // Or handle accordingly
            }

            return cus;
        }
    }
}
