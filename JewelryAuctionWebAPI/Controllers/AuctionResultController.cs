using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.AuctionResult;
using JewelryAuctionData.DTO.Jewelry;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionResultController : ControllerBase
    {
        private readonly AuctionResultBusiness _auctionResultBusiness;

        public AuctionResultController()
        {
            _auctionResultBusiness = new AuctionResultBusiness();
        }

        //[Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _auctionResultBusiness.GetAll();
            if (result != null && result.Status > 0)
            {
                var AuctionResult = result.Data as List<AuctionResult>;
                return Ok(AuctionResult);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 3)
        {
            var result = await _auctionResultBusiness.GetPaged(pageNumber, pageSize);

            if (result.Status > 0 && result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        // [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _auctionResultBusiness.GetById(id);
            if (result.Status > 0 && result != null)
            {

                return Ok(result.Data);

            }
            else
            {
                return NotFound(result?.Message);
            }
        }


        //[Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string search1, string search2, string search3)
        {
            var result = await _auctionResultBusiness.Search(search1, search2, search3);

            if (result.Status > 0 && result != null)
            {
                var joinAuctions = result.Data as List<AuctionResult>;
                return Ok(joinAuctions);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpPost]
        [Route("CreateAuctionResult")]
        public async Task<IActionResult> CreateAuctionResult(CreateAuctionResultDTO createAuctionResult)
        {
            var result = await _auctionResultBusiness.CreateAuctionResult(createAuctionResult);
            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);

            }
            else
            {
                return BadRequest(result?.Message);
            }
        }

        //[Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("UpdateAuctionResult")]
        public async Task<IActionResult> UpdateJewelry(UpdateAuctionResultDTO updateAuctionResult)
        {
            var rs = await _auctionResultBusiness.UpdateAuctionResult(updateAuctionResult);
            if (rs.Status > 0 && rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest(rs?.Message);
            }
        }

        //[Authorize(Roles = "Customer")]
        [HttpDelete]
        [Route("DeleteAuctionResult")]
        public async Task<IActionResult> DeleteAuctionResult(int AuctionResultId)
        {
            var rs = await _auctionResultBusiness.DeleteAuctionResult(AuctionResultId);
            if (rs.Status > 0 && rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest(rs?.Message);
            }
        }
    }
}
