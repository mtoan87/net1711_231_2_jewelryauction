using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.Bid
{
    public class CreateBidDTO
    {
        public int? CustomerId { get; set; }

        public int? JoinAuctionId { get; set; }

        public double? BidAmount { get; set; }

        public DateTime? DateTime { get; set; }

        public int? JewelryId { get; set; }

        public string? BidderName { get; set; }

        public string? JoinAuctionName { get; set; }

        public string? JoinAuctionDescription { get; set; }

        public string? BidStatus { get; set; }

        public string? IsWining { get; set; }
    }
}
