﻿using Common;
using JewelryAuctionData.DTO.Payment;
using JewelryAuctionData.Models;
using JewelryAuctionData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryAuctionData.DTO.Jewelry;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace JewelryAuctionBusiness
{
    public class JewelryBusiness
    {
        private readonly UnitOfWork _unitOfWork;


        public JewelryBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var jewelry = await _unitOfWork.JewelryRepository.GetAllAsync();

                if (jewelry == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Get jewelry list fail!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get jewelry list success", jewelry);
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
                var jewelry = await _unitOfWork.JewelryRepository.GetByIdAsync(code);
                if (jewelry == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No jewelry date by  code!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, " Get jewelry success", jewelry);
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
                var joinAuctions = await _unitOfWork.JewelryRepository.GetByConditionAsync(
                    a => a.JewelryName.Contains(search1) ||
                    a.Material.Contains(search2) ||
                    a.Type.Contains(search3));
                

                


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
        public async Task<JewelryAuctionResult> CreateJewelry(CreateJewelryDTO createJewelry)
        {
            try
            {
                var newJewelry = new Jewelry
                {
                    JewelryId = createJewelry.JewelryId,
                    JewelryName = createJewelry.JewelryName,
                    Material = createJewelry.Material,
                    Size = createJewelry.Size,
                    Weight = createJewelry.Weight,
                    CustomerId = createJewelry.CustomerId,
                    PictureData = createJewelry.PictureData,
                    Type = createJewelry.Type,
                    Quantitative = createJewelry.Quantitative,
                    Description = createJewelry.Description,
                    Status = createJewelry.Status,
                    Picture = createJewelry.Picture,

                };
                var jewelry = await _unitOfWork.JewelryRepository.CreateAsync(newJewelry);


                if (jewelry == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, " Error!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Create success!", jewelry);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> DeleteJewelry(int jewelryId)
        {
            try
            {
                var jewelry = await _unitOfWork.JewelryRepository.GetByIdAsync(jewelryId);
                if (jewelry == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Jewelry not found.");
                }

                await _unitOfWork.JewelryRepository.RemoveAsync(jewelry);
                return new JewelryAuction(Const.SUCCESS_GET, "Jewelry deleted successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> UpdateJewelry(UpdateJewelryDTO updateJewelry)
        {
            try
            {
                var jewelry = await _unitOfWork.JewelryRepository.GetByIdAsync(updateJewelry.JewelryId);
                if (jewelry == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Jewelry not found.");
                }

                jewelry.CustomerId = updateJewelry.CustomerId;
                jewelry.JewelryName = updateJewelry.JewelryName;
                jewelry.Material = updateJewelry.Material;
                jewelry.Size = updateJewelry.Size;
                jewelry.Weight = updateJewelry.Weight;
                jewelry.JewelryId = updateJewelry.JewelryId;
                jewelry.PictureData = updateJewelry.PictureData;
                jewelry.Type = updateJewelry.Type;
                jewelry.Quantitative = updateJewelry.Quantitative;
                jewelry.Description = updateJewelry.Description;
                jewelry.Status = updateJewelry?.Status;
                jewelry.Picture = updateJewelry.Picture;


                _unitOfWork.JewelryRepository.UpdateAsync(jewelry);
                return new JewelryAuction(Const.SUCCESS_GET, "jewelry updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
