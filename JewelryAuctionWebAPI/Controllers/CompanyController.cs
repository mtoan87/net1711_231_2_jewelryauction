using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Customer;
using JewelryAuctionData.DTO;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JewelryAuctionData.DTO.Company;

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
                var customer = result.Data as List<Company>;
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

        [HttpPost]
        [Route("CreateCompany")]
        public async Task<IActionResult> CreateCustomer(CreateCompanyDTO createCompany)
        {
            var result = await _companyBusiness.CreateCompany(createCompany);
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
        [Route("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyDTO updateCompany)
        {
            var rs = await _companyBusiness.UpdateCompany(updateCompany);
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
        [Route("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var rs = await _companyBusiness.DeleteCompany(companyId);
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
