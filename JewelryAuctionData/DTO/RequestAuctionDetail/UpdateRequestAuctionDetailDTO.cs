﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO
{
    public class UpdateRequestAuctionDetailDTO
    {
        public int RequestAuctionDetailId { get; set; }
        public int CustomerId { get; set; }
        public int JewelryId { get; set; }
        public int RequestAuctionId { get; set; }
        
    }
}
