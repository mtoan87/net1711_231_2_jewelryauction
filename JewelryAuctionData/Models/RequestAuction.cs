using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class RequestAuction
{
    public int RequestAuctionId { get; set; }

    public string? AuctionName { get; set; }

    public int? CustomerId { get; set; }

    public string? Status { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? JewelryReceived { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public int? SellerConfirmation { get; set; }

    public DateTime? FinalEstimateSentAt { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<RequestAuctionDetail> RequestAuctionDetails { get; set; } = new List<RequestAuctionDetail>();
}
