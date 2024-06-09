using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Jewelry;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
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
