using JewelryAuctionData.Base;
using JewelryAuctionData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData.Repository
{
    public class JoinAuctionRepository : GenericRepository<JoinAuction>
    {
        public JoinAuctionRepository()
        {

        }
        public JoinAuctionRepository(Net17112312JewelryAuctionContext context) => _context = context;
    }
}
