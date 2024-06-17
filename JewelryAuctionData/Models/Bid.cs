using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class Bid
{
    public int BidId { get; set; }

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

    public virtual ICollection<AuctionResult> AuctionResults { get; set; } = new List<AuctionResult>();

    public virtual Jewelry? Jewelry { get; set; }

    public virtual JoinAuction? JoinAuction { get; set; }
}
