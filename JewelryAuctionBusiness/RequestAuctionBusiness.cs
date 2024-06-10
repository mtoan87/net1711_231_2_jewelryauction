using Common;
using JewelryAuctionData;
using JewelryAuctionData.DAO;
using JewelryAuctionData.DTO;
using JewelryAuctionData.Models;
using System;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{
    public class RequestAuctionBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public RequestAuctionBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }

        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var requestAuctions = await _unitOfWork.RequestAuctionRepository.GetAllAsync();
                if (requestAuctions == null || !requestAuctions.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No request auction data");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get request auction list success", requestAuctions);
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
                var requestAuction = await _unitOfWork.RequestAuctionRepository.GetByIdAsync(id);
                if (requestAuction == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No request auction data by ID");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get request auction success", requestAuction);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> Create(CreateRequestAuctionDTO createAuction)
        {
            try
            {
                var newAuction = new RequestAuction 
                {
                    AuctionName = createAuction.AuctionName,
                    CustomerId = createAuction.CustomerId,
                    Status = createAuction.Status,
                    StartTime = createAuction.StartTime,
                    EndTime = createAuction.EndTime,
                };
                var result = await _unitOfWork.RequestAuctionRepository.CreateAsync(newAuction);
                if (result > 0)
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Create request auction success", createAuction);
                }
                else
                {
                    return new JewelryAuction(Const.ERROR_EXCEPTION, "Create request auction failed");
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        

        public async Task<JewelryAuctionResult> Update(UpdateRequestAuctionDTO updateAuction)
        {
            try
            {
                var auction = await _unitOfWork.RequestAuctionRepository.GetByIdAsync(updateAuction.RequestAuctionId);
                auction.AuctionName = updateAuction.AuctionName;
                auction.Status = updateAuction.Status;
                auction.StartTime = updateAuction.StartTime;
                auction.EndTime = updateAuction.EndTime;
                await _unitOfWork.RequestAuctionRepository.UpdateAsync(auction);
                return new JewelryAuction(Const.SUCCESS_GET, "Update request auction success", auction);
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> Delete(int id)
        {
            try
            {
                var requestAuction = await _unitOfWork.RequestAuctionRepository.GetByIdAsync(id);
                if (requestAuction == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No request auction data by ID");
                }

                var result = await _unitOfWork.RequestAuctionRepository.RemoveAsync(requestAuction);
                if (result)
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Delete request auction success");
                }
                else
                {
                    return new JewelryAuction(Const.ERROR_EXCEPTION, "Delete request auction failed");
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
