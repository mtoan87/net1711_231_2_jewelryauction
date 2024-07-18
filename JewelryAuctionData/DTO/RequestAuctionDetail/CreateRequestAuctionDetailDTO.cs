using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO
{
    public class CreateRequestAuctionDetailDTO
    {
        public int? CustomerId { get; set; }

        public int? JewelryId { get; set; }

        public int? RequestAuctionId { get; set; }

        public string? JewelryName { get; set; }

        public string? JewelryDescription { get; set; }

        public double? InitialEstimate { get; set; }

        public double? FinalEstimate { get; set; }

        public string? Status { get; set; }

        public DateTime? ReceivedAt { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
