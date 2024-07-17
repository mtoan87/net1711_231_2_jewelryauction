using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Customer;
using JewelryAuctionData.DTO;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JewelryAuctionData.DTO.Company;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string search)
        {
            var result = await _companyBusiness.Search(search);

            if (result.Status > 0 && result != null)
            {
                var company = result.Data as List<Company>;
                return Ok(company);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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
        
        [HttpGet]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 3)
        {
            var result = await _companyBusiness.GetPaged(pageNumber, pageSize);

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
