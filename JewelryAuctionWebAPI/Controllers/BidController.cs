using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Bid;
using JewelryAuctionData.DTO.JoinAuction;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelryAuctionWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidController : ControllerBase
    {
        private readonly BidBusiness bidBusiness;

        public BidController()
        {
            bidBusiness = new BidBusiness();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await bidBusiness.GetAll();

            if (result.Status > 0 && result != null)
            {
                var bid = result.Data as List<Bid>;
                return Ok(bid);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await bidBusiness.GetById(id);

            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string search)
        {
            var result = await bidBusiness.Search(search);

            if (result.Status > 0 && result != null)
            {
                var bids = result.Data as List<Bid>;
                return Ok(bids);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateBid(CreateBidDTO createBid)
        {
            var result = await bidBusiness.CreateBid(createBid);

            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateBid(UpdateBidDTO updateBid)
        {
            var result = await bidBusiness.UpdateBid(updateBid);

            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteBid(int bidId)
        {
            var result = await bidBusiness.DeleteBid(bidId);

            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
