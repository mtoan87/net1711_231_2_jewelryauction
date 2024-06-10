﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.Bid
{
    public class CreateBidDTO
    {
        public int? CustomerId { get; set; }

        public int? JoinAuctionId { get; set; }

        public double? MinPrice { get; set; }

        public double? MaxPrice { get; set; }

        public DateTime? DateTime { get; set; }
    }
}