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

        public async Task<IActionResult> Search(string search)
        {
            var result = await _requestAuctionDetailBusiness.Search(search);

            if (result.Status > 0 && result != null)
            {
                var auctions = result.Data as List<RequestAuctionDetail>;
                return Ok(auctions);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        
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
