using JewelryAuctionData.Base;
using JewelryAuctionData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.Repository
{
    public class RequestAuctionRepository : GenericRepository<RequestAuction>
    {
        public RequestAuctionRepository() 
        {
        }
        public RequestAuctionRepository(Net17112312JewelryAuctionContext context) => _context = context;
        public void AddRequestAuction(RequestAuction requestAuction)
        {
            _context.RequestAuctions.Add(requestAuction);
            _context.SaveChanges();
        }
    }
}
