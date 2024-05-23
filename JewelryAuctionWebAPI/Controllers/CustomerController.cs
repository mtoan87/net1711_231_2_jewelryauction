using JewelryAuctionBusiness;

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
        public async Task<IActionResult> CreateCustomer(Customer createCustomer)
        {
            var result = new Customer
            {
                CustomerId = createCustomer.CustomerId,
                CustomerName = createCustomer.CustomerName,
                Email = createCustomer.Email,
                Address = createCustomer.Address,
                Phone = createCustomer.Phone,
                Gender = createCustomer.Gender,

            };
             _customerBusiness.CreateCustomer(result);
            return Ok("Account created successfully.");
        }
    }
}
    

