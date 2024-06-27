using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.DTO.Customer
{
    public class UpdateCustomerDTO
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
    }
}
