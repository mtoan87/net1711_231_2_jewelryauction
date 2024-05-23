using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class AuctionResult
{
    public int AuctionResultId { get; set; }

    public DateTime? Date { get; set; }

    public string? Status { get; set; }

    public double? Price { get; set; }

    public int? CustomerId { get; set; }

    public int? JoinAuctionId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
