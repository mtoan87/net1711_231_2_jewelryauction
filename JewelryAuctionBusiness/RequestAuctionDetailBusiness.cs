using Common;
using JewelryAuctionData;
using JewelryAuctionData.DAO;
using JewelryAuctionData.DTO;
using JewelryAuctionData.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{
    public class RequestAuctionDetailBusiness
    {
        private readonly UnitOfWork _unitOfWork;

        public RequestAuctionDetailBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var requestAuctionDetails = await _unitOfWork.RequestAuctionDetailRepository.GetAllAsync();
                if (requestAuctionDetails == null || !requestAuctionDetails.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No request auction detail data");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get request auction detail list success", requestAuctionDetails);
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
                var requestAuctionDetail = await _unitOfWork.RequestAuctionDetailRepository.GetByIdAsync(id);
                if (requestAuctionDetail == null)
                {
                    return new  JewelryAuction(Const.WARINING_NO_DATA, "No request auction detail data by ID");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get request auction detail success", requestAuctionDetail);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> Create(CreateRequestAuctionDetailDTO createDetail)
        {
            try
            {
                var newDetail = new RequestAuctionDetail 
                {
                    CustomerId = createDetail.CustomerId,
                    JewelryId = createDetail.JewelryId,
                    RequestAuctionId = createDetail.RequestAuctionId,
                };
                var result = await _unitOfWork.RequestAuctionDetailRepository.CreateAsync(newDetail);
                if (result > 0)
                {
                    return new JewelryAuction(Const.SUCCESS_CREATE, "Create request auction detail success", newDetail);
                }
                else
                {
                    return new JewelryAuction(Const.ERROR_CREATE, "Create request auction detail failed");
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> Update(UpdateRequestAuctionDetailDTO updateDetail)
        {
            try
            {
                var detail = await _unitOfWork.RequestAuctionDetailRepository.GetByIdAsync(updateDetail.RequestAuctionDetailId);
                detail.CustomerId = updateDetail.CustomerId;
                detail.JewelryId = updateDetail.JewelryId;
                detail.RequestAuctionId = updateDetail.RequestAuctionId;
                await _unitOfWork.RequestAuctionDetailRepository.UpdateAsync(detail);
                return new JewelryAuction(Const.SUCCESS_UPDATE, "Update request auction detail success", detail);
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
                var requestAuctionDetail = await _unitOfWork.RequestAuctionDetailRepository.GetByIdAsync(id);
                if (requestAuctionDetail == null)
                {
                    return new  JewelryAuction(Const.WARINING_NO_DATA, "No request auction detail data by ID");
                }

                var result = await _unitOfWork.RequestAuctionDetailRepository.RemoveAsync(requestAuctionDetail);
                if (result)
                {
                    return new JewelryAuction(Const.SUCCESS_DELETE, "Delete request auction detail success");
                }
                else
                {
                    return new JewelryAuction(Const.ERROR_DELETE, "Delete request auction detail failed");
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
