﻿using System;
using System.Collections.Generic;

namespace JewelryAuctionData.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }
}
