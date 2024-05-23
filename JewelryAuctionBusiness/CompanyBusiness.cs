using Common;
using JewelryAuctionData.DAO;
using JewelryAuctionData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{

    public class CompanyBusiness
    {
        private readonly CompanyDAO _DAO;


        public CompanyBusiness()
        {
            _DAO = new CompanyDAO();
        }

        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var company = await _DAO.GetAllAsync();
                if (company == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Get company list fail!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get company list success", company);
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
                var company = await _DAO.GetByIdAsync(code);
                if (company == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No company date by  code!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, " Get company success", company);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);

            }

        }
    }
}
