using JewelryAuctionData.Models;
using JewelryAuctionWebAPI.Contracts.Login;

namespace JewelryAuctionWebAPI.JwtServices.IServices
{
    public interface IAuthService
    {
        Task<Customer> Login(LoginRequest loginRequest);
    }
}
