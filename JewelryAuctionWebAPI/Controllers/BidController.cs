using Common;
using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Bid;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 3)
        {
            var result = await bidBusiness.GetPaged(pageNumber, pageSize);

            if (result.Status > 0 && result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetBidsByJoinAuctionId")]
        public async Task<IActionResult> GetBidsByJoinAuctionId(int joinAuctionId)
        {
            var result = await bidBusiness.GetBidsByJoinAuctionId(joinAuctionId);
            if (result.Status == Const.SUCCESS_GET)
            {
                return Ok(result.Data);
            }
            return NotFound(result.Message);
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> FilterBids(double? bidAmount, DateTime? dateTime, int? jewelryId)
        {
            var result = await bidBusiness.FilterBids(bidAmount, dateTime, jewelryId);

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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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
