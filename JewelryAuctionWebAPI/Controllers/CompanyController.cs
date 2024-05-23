using JewelryAuctionBusiness;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyBusiness _companyBusiness;

        public CompanyController()
        {
            _companyBusiness = new CompanyBusiness();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyBusiness.GetAll();
            if (result != null && result.Status > 0)
            {
                var customer = result.Data as List<Customer>;
                return Ok(customer);
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
            var result = await _companyBusiness.GetById(id);
            if (result.Status > 0 && result != null)
            {

                return Ok(result.Data);

            }
            else
            {
                return NotFound(result?.Message);
            }
        }


    }
}
