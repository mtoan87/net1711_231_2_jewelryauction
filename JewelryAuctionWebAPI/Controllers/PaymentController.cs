using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentBusiness _paymentBusiness;

        public PaymentController()
        {
            _paymentBusiness = new PaymentBusiness();
        }

        /*[Authorize(Roles = "Customer")]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(string? search, string? status, string? paymentMethod, float? minPrice = null, float? maxPrice = null)
        {
            try
            {
                var result = await _paymentBusiness.GetAll(search);

                if (result != null && result.Status > 0)
                {
                    var payments = result.Data as List<Payment>;

                    // Apply filters
                    if (!string.IsNullOrEmpty(status))
                    {
                        payments = payments.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (!string.IsNullOrEmpty(paymentMethod))
                    {
                        payments = payments.Where(p => p.PaymentMethod.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (minPrice != null)
                    {
                        payments = payments.Where(p => p.TotalPrice >= minPrice).ToList();
                    }

                    if (maxPrice != null)
                    {
                        payments = payments.Where(p => p.TotalPrice <= maxPrice).ToList();
                    }

                    return Ok(payments);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*[Authorize(Roles = "Customer")]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _paymentBusiness.GetById(id);
            if (result.Status > 0 && result != null)
            {

                return Ok(result.Data);

            }
            else
            {
                return NotFound(result?.Message);
            }
        }

        /*[Authorize(Roles = "Customer")]*/
        [AllowAnonymous]
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePaymennt(CreatePaymentDTO createPayment)
        {
            var result = await _paymentBusiness.CreatePayment(createPayment);
            if (result.Status > 0 && result != null)
            {
                return Ok(result.Data);

            }
            else
            {
                return BadRequest(result?.Message);
            }
        }

        /*[Authorize(Roles = "Customer")]*/
        [AllowAnonymous]
        [HttpPost]
        [Route("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(UpdatePaymentDTO updatePayment)
        {
            var rs = await _paymentBusiness.UpdatePayment(updatePayment);
            if (rs.Status > 0 && rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest(rs?.Message);
            }
        }

        /*[Authorize(Roles = "Customer")]*/
        [AllowAnonymous]
        [HttpDelete]
        [Route("DeletePayment")]
        public async Task<IActionResult> DeletePayment(int paymentId)
        {
            var rs = await _paymentBusiness.DeletePayment(paymentId);
            if (rs.Status > 0 && rs != null)
            {
                return Ok(rs.Data);
            }
            else
            {
                return BadRequest(rs?.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> FilterPayments(double? price, DateTime? date, int? jewelryId)
        {
            try
            {
                var result = await _paymentBusiness.FilterPayments(price, date, jewelryId);

                if (result.Status > 0 && result != null)
                {
                    var payments = result.Data as List<Payment>;
                    return Ok(payments);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string search)
        {
            try
            {
                var result = await _paymentBusiness.Search(search);

                if (result.Status > 0 && result != null)
                {
                    var payments = result.Data as List<Payment>;
                    return Ok(payments);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }   
}
