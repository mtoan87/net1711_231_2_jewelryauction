using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class JoinAuction
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

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual Customer? Customer { get; set; }
}
