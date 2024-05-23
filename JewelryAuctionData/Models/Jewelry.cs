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

    public virtual ICollection<RequestAuctionDetail> RequestAuctionDetails { get; set; } = new List<RequestAuctionDetail>();
}
