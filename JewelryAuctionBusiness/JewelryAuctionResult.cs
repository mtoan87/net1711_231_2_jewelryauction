using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{
    public interface JewelryAuctionResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }

    public class JewelryAuction : JewelryAuctionResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public JewelryAuction()
        {
            Status = -1;
            Message = "Action fail";
        }
        public JewelryAuction(int status, string message)
        {
            Status = status;
            Message = message;
        } 
        public JewelryAuction(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
