using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.JoinAuction;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 3)
        {
            var result = await joinAuctionBusiness.GetPaged(pageNumber, pageSize);

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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> FilterJoinAuctions(int? customerId, DateTime? startTime, DateTime? endTime)
        {
            var result = await joinAuctionBusiness.FilterJoinAuctions(customerId, startTime, endTime);

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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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
