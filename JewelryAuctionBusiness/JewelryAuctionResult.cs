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
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        int PageSize { get; set; }
    }

    public class JewelryAuction : JewelryAuctionResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
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

        public JewelryAuction(int status, string message, object data, int totalPages, int currentPage, int pageSize)
        {
            Status = status;
            Message = message;
            Data = data;
            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
