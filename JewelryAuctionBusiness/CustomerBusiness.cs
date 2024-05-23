using Common;
using JewelryAuctionData.DAO;

using JewelryAuctionData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionBusiness
{
    
    public class CustomerBusiness 

    {
        private readonly CustomerDAO _DAO;

      
        public CustomerBusiness()
        {
            _DAO = new CustomerDAO();
        }
        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var customer = await _DAO.GetAllAsync();
                if(customer == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Get customer list fail!");
                } else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Get customer list success", customer);
                }
            }catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<JewelryAuctionResult> GetById(int code)
        {
            try
            {
                var customer = await _DAO.GetByIdAsync(code);
                if (customer == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No customer date by  code!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, " Get customer success", customer);
                }
            } catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);

            }
        
        }
        public async Task<JewelryAuctionResult> CreateCustomer(Customer createCustomer)
        {
            try
            {

                var customer = await _DAO.CreateAsync(createCustomer);
                

                if (customer == null) 
                {
                    return new JewelryAuction(Const.DUPLICATE,"Duplicated Id");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Creat success", customer);
                }
            }
            catch (Exception ex) {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        
    }
    


}
