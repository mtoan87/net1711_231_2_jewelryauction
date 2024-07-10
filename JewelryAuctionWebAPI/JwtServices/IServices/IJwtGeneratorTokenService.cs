using JewelryAuctionData.Models;

namespace JewelryAuctionWebAPI.JwtServices.IServices
{
    public interface IJwtGeneratorTokenService
    {
        string GenerateToken(Customer cus);
    }
}
