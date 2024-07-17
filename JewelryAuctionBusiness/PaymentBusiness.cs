using Common;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{
    public class PaymentBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public PaymentBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<JewelryAuctionResult> GetAll(string search)
        {
            try
            {
                var payments = await _unitOfWork.PaymentRepository.GetAllAsync();

                if (!string.IsNullOrEmpty(search))
                {
                    payments = payments.Where(p =>
                        p.PaymentMethod.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        p.Status.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        p.TotalPrice.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                        p.Price.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }
               

                if (payments == null || !payments.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No payments found.", null);
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Payments retrieved successfully.", payments);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message, null);
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

        public async Task<JewelryAuctionResult> Search(string search)
        {
            try
            {
                var payments = await _unitOfWork.PaymentRepository.GetByConditionAsync(
                payment => payment.Status.Contains(search) ||
               payment.PaymentMethod.Contains(search) ||
               payment.Price.ToString().Contains(search) ||
               payment.TotalPrice.ToString().Contains(search));

                if (payments == null || !payments.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No customer found with the given search term");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Customer search success", payments);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> FilterPayments(double? price, DateTime? date, int? jewelryId)
        {
            try
            {
                var payments = await _unitOfWork.PaymentRepository.GetByConditionAsync(
                    p => (!price.HasValue || p.Price == price.Value) &&
                         (!date.HasValue || p.Date >= date.Value) &&
                         (!jewelryId.HasValue || p.JewelryId == jewelryId.Value));

                if (payments == null || !payments.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No payments found with the given criteria");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Payments filtered successfully", payments);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


    }
}
