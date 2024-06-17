using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.JoinAuction
{
    public class UpdateJoinAuctionDTO
    {
        public int JoinAuctionId { get; set; }

        public int? CustomerId { get; set; }

        public int? AuctionDetailId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Host { get; set; }

        public string? JoinAuctionName { get; set; }

        public string? JoinAuctionDescription { get; set; }

        public string? JoinAuctionStatus { get; set; }

        public string? IsLive { get; set; }
    }
}
