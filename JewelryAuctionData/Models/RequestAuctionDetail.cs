using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class RequestAuctionDetail
{
    public int RequestAuctionDetailId { get; set; }

    public int? CustomerId { get; set; }

    public int? JewelryId { get; set; }

    public int? RequestAuctionId { get; set; }

    public virtual Jewelry? Jewelry { get; set; }

    public virtual RequestAuction? RequestAuction { get; set; }
}
