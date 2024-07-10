using JewelryAuctionData.Models;

namespace JewelryAuctionWebAPI.Contracts.Login
{
    public class LoginResponse
    {
        public Customer Customer { get; set; }
        public string Token { get; set; }
    }
}
