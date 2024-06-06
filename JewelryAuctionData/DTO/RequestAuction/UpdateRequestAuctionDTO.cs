﻿using JewelryAuctionData.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO
{
    public class UpdateRequestAuctionDTO
    {
        public int RequestAuctionId { get; set; }
        public string? AuctionName { get; set; }
        public int? CustomerId { get; set; }
        public string? Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        
    }
}
