using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Jewelry;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelryController : ControllerBase
    {
        private readonly JewelryBusiness _jewelryBusiness;

        public JewelryController()
        {
            _jewelryBusiness = new JewelryBusiness();
        }

        //[Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _jewelryBusiness.GetAll();
            if (result != null && result.Status > 0)
            {
                var Jewelry = result.Data as List<Jewelry>;
                return Ok(Jewelry);
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
            var result = await _jewelryBusiness.GetPaged(pageNumber, pageSize);

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
            var result = await _jewelryBusiness.GetById(id);
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
        public async Task<IActionResult> Search(string search)
        {
            var result = await _jewelryBusiness.Search(search);

            if (result.Status > 0 && result != null)
            {
                var joinAuctions = result.Data as List<Jewelry>;
                return Ok(joinAuctions);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpPost]
        [Route("CreateJewelry")]
        public async Task<IActionResult> CreateJewelry(CreateJewelryDTO createJewelry)
        {
            var result = await _jewelryBusiness.CreateJewelry(createJewelry);
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
        [Route("UpdateJewelry")]
        public async Task<IActionResult> UpdateJewelry(UpdateJewelryDTO updateJewelry)
        {
            var rs = await _jewelryBusiness.UpdateJewelry(updateJewelry);
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
        [Route("DeleteJewelry")]
        public async Task<IActionResult> DeleteJewelry(int jewelryId)
        {
            var rs = await _jewelryBusiness.DeleteJewelry(jewelryId);
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
