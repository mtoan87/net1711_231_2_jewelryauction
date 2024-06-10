using Common;
using JewelryAuctionData;
using JewelryAuctionData.DTO.Bid;
using JewelryAuctionData.DTO.JoinAuction;
using JewelryAuctionData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{
    public class BidBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public BidBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var bids = await _unitOfWork.bidRepository.GetAllAsync();

                if (bids == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No join auction");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Join auction success", bids);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> GetById(int id)
        {
            try
            {
                var bid = await _unitOfWork.bidRepository.GetByIdAsync(id);
                if (bid == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bid");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Success", bid);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> CreateBid(CreateBidDTO createBid)
        {
            try
            {
                var bid = new Bid()
                {
                    CustomerId = createBid.CustomerId,
                    JoinAuctionId = createBid.JoinAuctionId,
                    MinPrice = createBid.MinPrice,
                    MaxPrice = createBid.MaxPrice,
                    DateTime = createBid.DateTime,

                };
                var result = await _unitOfWork.bidRepository.CreateAsync(bid);

                if (result == 0)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Failed to create bid");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Create bid success", result);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> UpdateBid(UpdateBidDTO updateBid)
        {
            try
            {
                var bid = await _unitOfWork.bidRepository.GetByIdAsync(updateBid.BidId);
                if (bid == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bid found");
                }

                bid.CustomerId = updateBid.CustomerId;
                bid.MaxPrice = updateBid.MaxPrice;
                bid.MinPrice = updateBid.MinPrice;
                bid.DateTime = updateBid.DateTime;

                _unitOfWork.bidRepository.UpdateAsync(bid);
                return new JewelryAuction(Const.SUCCESS_GET, "Bid updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> DeleteBid(int bidId)
        {
            try
            {
                var bid = await _unitOfWork.bidRepository.GetByIdAsync(bidId);
                if (bid == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Bid not found");
                }
                await _unitOfWork.bidRepository.RemoveAsync(bid);
                return new JewelryAuction(Const.SUCCESS_GET, "Bid deleted successfully!");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
