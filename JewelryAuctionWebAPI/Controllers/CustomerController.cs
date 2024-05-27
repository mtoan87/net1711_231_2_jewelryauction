using JewelryAuctionBusiness;
using JewelryAuctionData.DTO;
using JewelryAuctionData.DTO.Customer;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBusiness _customerBusiness;

        public CustomerController()
        {
            _customerBusiness = new CustomerBusiness();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerBusiness.GetAll();
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
            var result = await _customerBusiness.GetById(id);
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
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO createCustomer)
        {
            var result = await _customerBusiness.CreateCustomer(createCustomer);
            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);

            }
            else
            {
                return BadRequest(result?.Message);
            }
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDTO updateCustomer)
        {
            var rs = await _customerBusiness.UpdateCustomer(updateCustomer);
            if(rs.Status > 0 && rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest(rs?.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var rs = await _customerBusiness.DeleteCustomer(customerId);
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
    

