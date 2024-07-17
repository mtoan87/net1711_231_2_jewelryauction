using Common;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryAuctionData.DTO.AuctionResult;

namespace JewelryAuctionBusiness
{
    public class AuctionResultBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public AuctionResultBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var auctionResult = await _unitOfWork.AuctionResultRepository.GetAllAsync();

                if (auctionResult == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Get auction result list fail!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get auction result list success", auctionResult);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var auctionResult  = await _unitOfWork.AuctionResultRepository.GetPagedAsync(pageNumber, pageSize);
                var totalRecords = await _unitOfWork.AuctionResultRepository.CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                if (auctionResult == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No Auction Result");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Auction Result success", auctionResult, totalPages, pageNumber, pageSize);
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
                var auctionResult = await _unitOfWork.AuctionResultRepository.GetByIdAsync(code);
                if (auctionResult == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No auction result date by code!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, " Get auction result success", auctionResult);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);

            }

        }

        public async Task<JewelryAuctionResult> Search(string search1, string search2, string search3)
        {
            try
            {
                var joinAuctions = await _unitOfWork.AuctionResultRepository.GetByConditionAsync(
                    a => a.Jewelry.JewelryName.Contains(search1) ||
                    a.Customer.CustomerName.Contains(search2) ||
                    a.Price.ToString().Contains(search3));

                if (joinAuctions == null || !joinAuctions.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No auction found with the given search term");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Auction search success", joinAuctions);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> CreateAuctionResult(CreateAuctionResultDTO createAuctionResult)
        {
            try
            {
                var newAuctionResult = new AuctionResult
                {
                    
                    AuctionResultId = createAuctionResult.AuctionResultId,
                    Date = createAuctionResult.Date,
                    Status = createAuctionResult.Status,
                    Price = createAuctionResult.Price,
                    CustomerId = createAuctionResult.CustomerId,
                    JoinAuctionId = createAuctionResult.JoinAuctionId,
                    Detail = createAuctionResult.Detail,
                    Description = createAuctionResult.Description,
                    StartingPrice = createAuctionResult.StartingPrice,
                    JewelryId = createAuctionResult.JewelryId,
                    BidId = createAuctionResult.BidId,


                };
                var payment = await _unitOfWork.AuctionResultRepository.CreateAsync(newAuctionResult);


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
        public async Task<JewelryAuctionResult> DeleteAuctionResult(int auctionResultId)
        {
            try
            {
                var auctionResult = await _unitOfWork.AuctionResultRepository.GetByIdAsync(auctionResultId);
                if (auctionResult == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Auction Result not found.");
                }

                await _unitOfWork.AuctionResultRepository.RemoveAsync(auctionResult);
                return new JewelryAuction(Const.SUCCESS_GET, "Auction Result deleted successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> UpdateAuctionResult(UpdateAuctionResultDTO updateAuctionResult)
        {
            try
            {
                var auctionResult = await _unitOfWork.AuctionResultRepository.GetByIdAsync(updateAuctionResult.AuctionResultId);
                if (auctionResult == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Auction Result not found.");
                }
                auctionResult.AuctionResultId = updateAuctionResult.AuctionResultId;
                auctionResult.Date = updateAuctionResult.Date;
                auctionResult.Status = updateAuctionResult.Status;
                auctionResult.Price = updateAuctionResult.Price;              
                auctionResult.CustomerId = updateAuctionResult.CustomerId;
                auctionResult.JoinAuctionId = updateAuctionResult.JoinAuctionId;
                auctionResult.Detail = updateAuctionResult.Detail;
                auctionResult.Description = updateAuctionResult.Description;
                auctionResult.StartingPrice = updateAuctionResult.StartingPrice;
                auctionResult.JewelryId = updateAuctionResult.JewelryId;
                auctionResult.BidId = updateAuctionResult.BidId;

                _unitOfWork.AuctionResultRepository.UpdateAsync(auctionResult);
                return new JewelryAuction(Const.SUCCESS_GET, "Auction Result updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
