using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.AuctionResult
{
    public class UpdateAuctionResultDTO
    {
        public int AuctionResultId { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }

        public double? Price { get; set; }

        public int? CustomerId { get; set; }

        public int? JoinAuctionId { get; set; }

        public string? Detail { get; set; }

        public string? Description { get; set; }

        public double? StartingPrice { get; set; }

        public int? JewelryId { get; set; }

        public int? BidId { get; set; }
    }
}
