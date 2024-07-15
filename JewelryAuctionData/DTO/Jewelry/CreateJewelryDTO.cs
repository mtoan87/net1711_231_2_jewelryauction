using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.Jewelry
{
    public class CreateJewelryDTO
    {
        public int JewelryId { get; set; }

        public string? JewelryName { get; set; }

        public string? Material { get; set; }

        public string? Size { get; set; }
        public string? Weight { get; set; }
        public int? CustomerId { get; set; }

        public byte[]? PictureData { get; set; }

        public string? Type { get; set; }

        public string? Quantitative { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

  

    }
}
