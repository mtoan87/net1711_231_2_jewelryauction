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
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bid");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get bid success", bids);
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
                var bids = await _unitOfWork.bidRepository.GetPagedAsync(pageNumber, pageSize);
                var totalRecords = await _unitOfWork.bidRepository.CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                if (bids == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bid");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get bid success", bids, totalPages, pageNumber, pageSize);
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

        public async Task<JewelryAuctionResult> GetBidsByJoinAuctionId(int joinAuctionId)
        {
            try
            {
                var bids = await _unitOfWork.bidRepository.GetBidsByJoinAuctionIdAsync(joinAuctionId);
                if (bids == null || !bids.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bids found for this auction");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Bids retrieved successfully", bids);
                }
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
                var searchTerms = search.Split(',');

                // Lấy tất cả các bid trước
                var allBids = await _unitOfWork.bidRepository.GetAllAsync();

                // Thực hiện lọc trong bộ nhớ
                var filteredBids = allBids.Where(b =>
                    searchTerms.Any(term =>
                        b.BidderName.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        b.JoinAuctionName.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        b.BidStatus.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        b.IsWining.ToString().Contains(term.Trim(), StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (!filteredBids.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bids found with the given search terms");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Bid search success", filteredBids);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> FilterBids(double? bidAmount, DateTime? dateTime, int? jewelryId)
        {
            try
            {
                var bids = await _unitOfWork.bidRepository.GetByConditionAsync(
                    ja => (!bidAmount.HasValue || ja.BidAmount == bidAmount.Value) &&
                          (!dateTime.HasValue || ja.DateTime == dateTime.Value) &&
                          (!jewelryId.HasValue || ja.JewelryId == jewelryId.Value));



                if (bids == null || !bids.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bids found with the given criteria");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Bids filtered successfully", bids);
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
                if ((createBid.BidStatus == "Accpeted" && createBid.IsWining != "Yes") ||
                    (createBid.BidStatus == "Pending" && createBid.IsWining != "No"))
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Invalid BidStatus and IsWining combination");
                }

                var bid = new Bid()
                {
                    CustomerId = createBid.CustomerId,
                    JoinAuctionId = createBid.JoinAuctionId,
                    BidAmount = createBid.BidAmount,
                    DateTime = createBid.DateTime,
                    JewelryId = createBid.JewelryId,
                    BidderName = createBid.BidderName,
                    JoinAuctionName = createBid.JoinAuctionName,
                    JoinAuctionDescription = createBid.JoinAuctionDescription,
                    BidStatus = createBid.BidStatus,
                    IsWining = createBid.IsWining

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
                if ((updateBid.BidStatus == "Accpeted" && updateBid.IsWining != "Yes") ||
                    (updateBid.BidStatus == "Pending" && updateBid.IsWining != "No"))
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Invalid BidStatus and IsWining combination");
                }

                var bid = await _unitOfWork.bidRepository.GetByIdAsync(updateBid.BidId);
                if (bid == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No bid found");
                }

                bid.CustomerId = updateBid.CustomerId;
                bid.JoinAuctionId = updateBid.JoinAuctionId;
                bid.BidAmount = updateBid.BidAmount;
                bid.DateTime = updateBid.DateTime;
                bid.JewelryId = updateBid.JewelryId;
                bid.BidderName = updateBid.BidderName;
                bid.JoinAuctionName = updateBid.JoinAuctionName;
                bid.JoinAuctionDescription = updateBid.JoinAuctionDescription;
                bid.BidStatus = updateBid.BidStatus;
                bid.IsWining = updateBid.IsWining;

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
