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

    public string? Detail { get; set; }

    public string? Description { get; set; }

    public double? StartingPrice { get; set; }

    public int? JewelryId { get; set; }

    public int? BidId { get; set; }

    public virtual Bid? Bid { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Jewelry? Jewelry { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
