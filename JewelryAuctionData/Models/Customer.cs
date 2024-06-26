using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public DateTime? DoB { get; set; }

    public string? Ocupation { get; set; }

    public string? Nationality { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<AuctionResult> AuctionResults { get; set; } = new List<AuctionResult>();

    public virtual ICollection<JoinAuction> JoinAuctions { get; set; } = new List<JoinAuction>();

    public virtual ICollection<RequestAuction> RequestAuctions { get; set; } = new List<RequestAuction>();
}
