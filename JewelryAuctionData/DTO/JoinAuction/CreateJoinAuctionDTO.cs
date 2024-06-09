﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.JoinAuction
{
    public class CreateJoinAuctionDTO
    {
        public int? CustomerId { get; set; }

        public int? AuctionDetailId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? Host { get; set; }
    }
}
