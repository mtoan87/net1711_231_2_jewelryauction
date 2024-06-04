using Common;
using JewelryAuctionData;
using JewelryAuctionData.DAO;
using JewelryAuctionData.DTO.Customer;
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
        private readonly UnitOfWork _unitOfWork;
        
      
        public CustomerBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }
        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetAllAsync();

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
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(code);
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
        public async Task<JewelryAuctionResult> CreateCustomer(CreateCustomerDTO createCustomer)
        {
            try
            {
                var newCustomer = new Customer
                {           
                    CustomerName = createCustomer.CustomerName,
                    Email = createCustomer.Email,
                    Phone = createCustomer.Phone,
                    Address = createCustomer.Address,
                    Gender = createCustomer.Gender,
                };
               var customer = await _unitOfWork.CustomerRepository.CreateAsync(newCustomer);
                

                if (customer == null) 
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA," Error!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Creat success!", customer);
                }
            }
            catch (Exception ex) {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> DeleteCustomer(int customerId)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
                if (customer == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Customer not found.");
                }

                await _unitOfWork.CustomerRepository.RemoveAsync(customer);
                return new JewelryAuction(Const.SUCCESS_GET, "Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> UpdateCustomer(UpdateCustomerDTO updateCustomer)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(updateCustomer.CustomerId);
                if (customer == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Customer not found.");
                }

                customer.CustomerName = updateCustomer.CustomerName;
                customer.Email = updateCustomer.Email;
                customer.Phone = updateCustomer.Phone;
                customer.Address = updateCustomer.Address;
                customer.Gender = updateCustomer.Gender;

                 _unitOfWork.CustomerRepository.UpdateAsync(customer);
                return new JewelryAuction(Const.SUCCESS_GET, "Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


    }
    


}
