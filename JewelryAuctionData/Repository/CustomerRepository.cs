using JewelryAuctionData.Base;
using JewelryAuctionData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.Repository
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        
        public CustomerRepository()
        {
        }
        public CustomerRepository(Net17112312JewelryAuctionContext context) => _context = context;
        
        public async Task<Customer> LoginAsync(string email, string password)
        {
           return  _context.Customers.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
        
    }
}
