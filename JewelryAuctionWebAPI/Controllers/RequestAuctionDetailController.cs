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
    public class RequestAuctionDetailController : ControllerBase
    {
        private readonly RequestAuctionDetailBusiness _requestAuctionDetailBusiness;

        public RequestAuctionDetailController()
        {
            _requestAuctionDetailBusiness = new RequestAuctionDetailBusiness();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _requestAuctionDetailBusiness.GetAll();
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
            var result = await _requestAuctionDetailBusiness.GetById(id);
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
        public async Task<IActionResult> Create([FromBody] CreateRequestAuctionDetailDTO createDetail)
        {
            var result = await _requestAuctionDetailBusiness.Create(createDetail);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRequestAuctionDetailDTO updateDetail)
        {
            if (id != updateDetail.RequestAuctionDetailId)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _requestAuctionDetailBusiness.Update(updateDetail);
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
            var result = await _requestAuctionDetailBusiness.Delete(id);
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
