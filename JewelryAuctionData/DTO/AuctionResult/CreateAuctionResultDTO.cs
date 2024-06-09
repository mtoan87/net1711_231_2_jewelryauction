using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.AuctionResult
{
    public class CreateAuctionResultDTO
    {
        public int AuctionResultId { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }

        public double? Price { get; set; }

        public int? CustomerId { get; set; }

        public int? JoinAuctionId { get; set; }

    }
}
