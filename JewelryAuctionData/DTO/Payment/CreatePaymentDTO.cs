using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.Payment
{
    public class CreatePaymentDTO
    {
        public int? CustomerId { get; set; }

        public int? AuctionResultId { get; set; }

        public string? Status { get; set; }

        public string? PaymentMethod { get; set; }

        public DateTime? Date { get; set; }

        public int? JewelryId { get; set; }

        public double? Fee { get; set; }

        public double? PercentFee { get; set; }

        public double? Price { get; set; }

        public double? TotalPrice { get; set; }
    }
}
