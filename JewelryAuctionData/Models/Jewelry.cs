using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class Jewelry
{
    public int JewelryId { get; set; }

    public string? JewelryName { get; set; }

    public string? Material { get; set; }

    public string? Size { get; set; }

    public string? Weight { get; set; }

    public int? CustomerId { get; set; }

    public byte[]? PictureData { get; set; }

    public string? Type { get; set; }

    public string? Quantitative { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<AuctionResult> AuctionResults { get; set; } = new List<AuctionResult>();

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<RequestAuctionDetail> RequestAuctionDetails { get; set; } = new List<RequestAuctionDetail>();
}
