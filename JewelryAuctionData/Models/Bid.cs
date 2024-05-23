using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class Bid
{
    public int BidId { get; set; }

    public int? CustomerId { get; set; }

    public int? JoinAuctionId { get; set; }

    public double? MinPrice { get; set; }

    public double? MaxPrice { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual JoinAuction? JoinAuction { get; set; }
}
