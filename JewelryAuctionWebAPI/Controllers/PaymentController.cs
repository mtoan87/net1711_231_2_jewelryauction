using JewelryAuctionBusiness;
using JewelryAuctionData.DTO.Customer;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
using Microsoft.AspNetCore.Mvc;

namespace JewelryAuctionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController: ControllerBase
    {
            private readonly PaymentBusiness _paymentBusiness;

            public PaymentController()
            {
                _paymentBusiness = new PaymentBusiness();
            }

            [HttpGet]
            [Route("GetAll")]
            public async Task<IActionResult> GetAll()
            {
                var result = await _paymentBusiness.GetAll();
                if (result != null && result.Status > 0)
                {
                    var payment = result.Data as List<Payment>;
                    return Ok(payment);
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
        }
}
