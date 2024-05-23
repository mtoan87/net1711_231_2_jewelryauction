using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

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

    public virtual AuctionResult? AuctionResult { get; set; }
}
