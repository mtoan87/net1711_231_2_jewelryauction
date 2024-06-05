using JewelryAuctionData.Base;
using JewelryAuctionData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.Repository
{
    public class AuctionResultRepository : GenericRepository<AuctionResult>
    {
        public AuctionResultRepository() { }

        public AuctionResultRepository(Net17112312JewelryAuctionContext context)
        {
            _context = context;
        }
    }
}
