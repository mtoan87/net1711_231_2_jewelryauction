﻿using System;
using System.Collections.Generic;

namespace JewelryAuctionWebAPI.Model;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? EstablishmentDate { get; set; }

    public string? Location { get; set; }

    public int? NumberOfEmployees { get; set; }

    public string? Industry { get; set; }
}
