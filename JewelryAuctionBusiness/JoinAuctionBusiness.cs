using Common;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryAuctionData.DTO.JoinAuction;

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
                var joinAuctions = await _unitOfWork.joinAuctionRepository.GetByConditionAsync(
                    a => a.Host.Contains(search) ||
                    a.JoinAuctionName.Contains(search) || 
                    a.JoinAuctionStatus.Contains(search));

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

        public async Task<JewelryAuctionResult> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction)
        {
            try
            {
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
