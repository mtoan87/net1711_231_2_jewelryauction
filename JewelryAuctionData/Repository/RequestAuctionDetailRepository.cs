using JewelryAuctionData.Base;
using JewelryAuctionData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.Repository
{
    public class RequestAuctionDetailRepository : GenericRepository<RequestAuctionDetail>
    {
        public RequestAuctionDetailRepository() 
        {
        }
        public RequestAuctionDetailRepository(Net17112312JewelryAuctionContext context) => _context = context;
    }
}
