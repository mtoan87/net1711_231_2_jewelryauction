using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Customer;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JewelryAuctionWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBusiness _customerBusiness;
        private readonly IConfiguration _config;
        
        public CustomerController(IConfiguration config)
        {
            
            _config = config;
            _customerBusiness = new CustomerBusiness();
        }

        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> Login(LoginDTO login)
        //{
        //    var cus = await _customerBusiness.LoginAsync(login.Email, login.Password);
        //    if (cus == null)
        //        return Unauthorized();

        //    var token = GenerateJSONWebToken(cus);
        //    return Ok(token);
        //}

        //private string GenerateJSONWebToken(Customer userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        _config["Jwt:Issuer"],
        //        _config["Jwt:Audience"],
        //        new Claim[]
        //        {
        //            new(ClaimTypes.Email, userInfo.Email),
        //            new(ClaimTypes.Role, userInfo.Role),
        //            new("customerId", userInfo.CustomerId.ToString()),
        //        },
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentials
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
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
        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string search)
        {
            var result = await _customerBusiness.Search(search);

            if (result.Status > 0 && result != null)
            {
                var customers = result.Data as List<Customer>;
                return Ok(customers);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        [Authorize(Roles = "Customer")]
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

        [Authorize(Roles = "Customer")]
        [HttpPost]
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

        [Authorize(Roles = "Customer")]
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
    

