using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO
{
    public class CreateRequestAuctionDTO
    {
        public string? AuctionName { get; set; }

        public int? CustomerId { get; set; }

        public string? Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? JewelryReceived { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public int? SellerConfirmation { get; set; }

        public DateTime? FinalEstimateSentAt { get; set; }
    }
}
