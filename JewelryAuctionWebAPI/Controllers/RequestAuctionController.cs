using JewelryAuctionBusiness;
using JewelryAuctionData.DTO;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JewelryAuctionWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestAuctionController : ControllerBase
    {
        private readonly RequestAuctionBusiness _requestAuctionBusiness;

        public RequestAuctionController()
        {
            _requestAuctionBusiness = new RequestAuctionBusiness();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _requestAuctionBusiness.GetAll();
            if (result != null && result.Status > 0)
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
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _requestAuctionBusiness.GetById(id);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] CreateRequestAuctionDTO createAuction)
        {
            var result = await _requestAuctionBusiness.Create(createAuction);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRequestAuctionDTO updateAuction)
        {
            if (id != updateAuction.RequestAuctionId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _requestAuctionBusiness.Update(updateAuction);
            if (result != null && result.Status > 0)
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
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _requestAuctionBusiness.Delete(id);
            if (result != null && result.Status > 0)
            {
                return Ok(result.Message);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
