using Common;
using JewelryAuctionData.DTO.Customer;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryAuctionData.DTO.Payment;

namespace JewelryAuctionBusiness
{
    public class PaymentBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public PaymentBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.GetAllAsync();

                if (payment == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Get payment list fail!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get payment list success", payment);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> GetById(int code)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(code);
                if (payment == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No payment date by  code!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, " Get payment success", payment);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);

            }

        }
        public async Task<JewelryAuctionResult> CreatePayment(CreatePaymentDTO createPayment)
        {
            try
            {
                var newPayment = new Payment
                {
                    CustomerId = createPayment.CustomerId,
                    AuctionResultId = createPayment.AuctionResultId,
                    Status = createPayment.Status,
                    PaymentMethod = createPayment.PaymentMethod,
                    Date = createPayment.Date,
                    JewelryId = createPayment.JewelryId,
                    Fee = createPayment.Fee,
                    PercentFee = createPayment.PercentFee,
                    Price = createPayment.Price,
                    TotalPrice = createPayment.TotalPrice,
                };
                var payment = await _unitOfWork.PaymentRepository.CreateAsync(newPayment);


                if (payment == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, " Error!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Create success!", payment);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> DeletePayment(int paymentId)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(paymentId);
                if (payment == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Payment not found.");
                }

                await _unitOfWork.PaymentRepository.RemoveAsync(payment);
                return new JewelryAuction(Const.SUCCESS_GET, "Payment deleted successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> UpdatePayment(UpdatePaymentDTO updatePayment)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(updatePayment.PaymentId);
                if (payment == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Payment not found.");
                }

                payment.CustomerId = updatePayment.CustomerId;
                payment.AuctionResultId = updatePayment.AuctionResultId;
                payment.Status = updatePayment.Status;
                payment.PaymentMethod = updatePayment.PaymentMethod;
                payment.Date = updatePayment.Date;
                payment.JewelryId = updatePayment.JewelryId;
                payment.Fee = updatePayment.Fee;
                payment.PercentFee = updatePayment.PercentFee;
                payment.Price = updatePayment.Price;
                payment.TotalPrice = updatePayment.TotalPrice;

                _unitOfWork.PaymentRepository.UpdateAsync(payment);
                return new JewelryAuction(Const.SUCCESS_GET, "Payment updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
