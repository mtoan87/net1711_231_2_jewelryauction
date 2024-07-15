using Common;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryAuctionData.DTO.JoinAuction;
using Microsoft.EntityFrameworkCore;

namespace JewelryAuctionBusiness
{
    public class JoinAuctionBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public JoinAuctionBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var joinAuctions = await _unitOfWork.joinAuctionRepository.GetAllAsync();

                if (joinAuctions == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No join auction");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Join auction success", joinAuctions);
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
                var joinAuctions = await _unitOfWork.joinAuctionRepository.GetPagedAsync(pageNumber, pageSize);
                var totalRecords = await _unitOfWork.joinAuctionRepository.CountAsync();

                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                if (joinAuctions == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No join auction");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Join auction success", joinAuctions, totalPages, pageNumber, pageSize);
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
                var joinAuctions = await _unitOfWork.joinAuctionRepository.GetByIdAsync(id);
                if (joinAuctions == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No join auction");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Join auction success", joinAuctions);
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
                // Tách các tiêu chí tìm kiếm bằng dấu phẩy
                var searchTerms = search.Split(',');

                // Lấy tất cả dữ liệu trước và thực hiện so sánh trong bộ nhớ
                var allJoinAuctions = await _unitOfWork.joinAuctionRepository.GetAllAsync();

                var filteredJoinAuctions = allJoinAuctions.Where(a =>
                    searchTerms.Any(term =>
                        a.Host.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        a.JoinAuctionName.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        a.JoinAuctionStatus.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        a.IsLive.ToString().Contains(term.Trim(), StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (!filteredJoinAuctions.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No auction found with the given search term");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Auction search success", filteredJoinAuctions);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> FilterJoinAuctions(int? customerId, DateTime? startTime, DateTime? endTime)
        {
            try
            {
                var joinAuctions = await _unitOfWork.joinAuctionRepository.GetByConditionAsync(
                    ja => (!customerId.HasValue || ja.CustomerId == customerId.Value) &&
                          (!startTime.HasValue || ja.StartTime == startTime.Value) &&
                          (!endTime.HasValue || ja.EndTime == endTime.Value));

                if (joinAuctions == null || !joinAuctions.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No join auctions found with the given criteria");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Join auctions filtered successfully", joinAuctions);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction)
        {
            try
            {
                if ((createJoinAuction.JoinAuctionStatus == "Opened" && createJoinAuction.IsLive != "Yes") ||
                    (createJoinAuction.JoinAuctionStatus == "Closed" && createJoinAuction.IsLive != "No"))
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Invalid JoinAuctionStatus and IsLive combination");
                }

                var joinAuction = new JoinAuction()
                {
                    CustomerId = createJoinAuction.CustomerId,
                    AuctionDetailId = createJoinAuction.AuctionDetailId,
                    StartTime = createJoinAuction.StartTime,
                    EndTime = createJoinAuction.EndTime,
                    Host = createJoinAuction.Host,
                    JoinAuctionName = createJoinAuction.JoinAuctionName,
                    JoinAuctionDescription = createJoinAuction.JoinAuctionDescription,
                    JoinAuctionStatus = createJoinAuction.JoinAuctionStatus,
                    IsLive = createJoinAuction.IsLive
                };
                var result = await _unitOfWork.joinAuctionRepository.CreateAsync(joinAuction);

                if (result == 0)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Failed to create auction");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Create auction success", result);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> UpdateJoinAuction(UpdateJoinAuctionDTO updateJoinAuction)
        {
            try
            {
                if ((updateJoinAuction.JoinAuctionStatus == "Opened" && updateJoinAuction.IsLive != "Yes") ||
                    (updateJoinAuction.JoinAuctionStatus == "Closed" && updateJoinAuction.IsLive != "No"))
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Invalid JoinAuctionStatus and IsLive combination");
                }

                var joinAuction = await _unitOfWork.joinAuctionRepository.GetByIdAsync(updateJoinAuction.JoinAuctionId);
                if (joinAuction == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No auction found");
                }

                joinAuction.CustomerId = updateJoinAuction.CustomerId;
                joinAuction.AuctionDetailId = updateJoinAuction.AuctionDetailId;
                joinAuction.StartTime = updateJoinAuction.StartTime;
                joinAuction.EndTime = updateJoinAuction.EndTime;
                joinAuction.Host = updateJoinAuction.Host;
                joinAuction.JoinAuctionName = updateJoinAuction.JoinAuctionName;
                joinAuction.JoinAuctionDescription = updateJoinAuction.JoinAuctionDescription;
                joinAuction.JoinAuctionStatus = updateJoinAuction.JoinAuctionStatus;
                joinAuction.IsLive = updateJoinAuction.IsLive;

                await _unitOfWork.joinAuctionRepository.UpdateAsync(joinAuction);
                return new JewelryAuction(Const.SUCCESS_GET, "Auction updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> DeleteJoinAuction(int joinAuctionId)
        {
            try
            {
                var joinAuction = await _unitOfWork.joinAuctionRepository.GetByIdAsync(joinAuctionId);
                if (joinAuction == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Auction not found");
                }
                await _unitOfWork.joinAuctionRepository.RemoveAsync(joinAuction);
                return new JewelryAuction(Const.SUCCESS_GET, "Auction deleted successfully!");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
