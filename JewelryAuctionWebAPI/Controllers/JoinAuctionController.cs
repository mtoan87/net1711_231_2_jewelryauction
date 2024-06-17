using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.JoinAuction;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JoinAuctionController : ControllerBase
    {
        private readonly JoinAuctionBusiness joinAuctionBusiness;

        public JoinAuctionController()
        {
            joinAuctionBusiness = new JoinAuctionBusiness();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await joinAuctionBusiness.GetAll();

            if (result.Status > 0 && result != null)
            {
                var joinAuctions = result.Data as List<JoinAuction>;
                return Ok(joinAuctions);
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
            var result = await joinAuctionBusiness.GetById(id);

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
            var result = await joinAuctionBusiness.Search(search);

            if (result.Status > 0 && result != null)
            {
                var joinAuctions = result.Data as List<JoinAuction>;
                return Ok(joinAuctions);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction)
        {
            var result = await joinAuctionBusiness.CreateJoinAuction(createJoinAuction);

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
        public async Task<IActionResult> UpdateJoinAuction(UpdateJoinAuctionDTO updateJoinAuction)
        {
            var result = await joinAuctionBusiness.UpdateJoinAuction(updateJoinAuction);

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
        public async Task<IActionResult> DeleteJoinAuction(int joinAuctionId)
        {
            var result = await joinAuctionBusiness.DeleteJoinAuction(joinAuctionId);

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
