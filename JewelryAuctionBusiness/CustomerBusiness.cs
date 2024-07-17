using Common;
using JewelryAuctionData;
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
        public async Task<Customer> LoginAsync(string email, string password)
        {
            return await _unitOfWork.CustomerRepository.LoginAsync(email, password);
        }

        public async Task<JewelryAuctionResult> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var bids = await _unitOfWork.CustomerRepository.GetPagedAsync(pageNumber, pageSize);
                var totalRecords = await _unitOfWork.CustomerRepository.CountAsync();

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
        public async Task<JewelryAuctionResult> Search(string search)
        {
            try
            {
                var searchTerms = search.Split(',');

                // Get all customers asynchronously
                var allCustomers = await _unitOfWork.CustomerRepository.GetAllAsync();

                // Perform in-memory filtering
                var filteredCustomers = allCustomers.Where(customer =>
                    searchTerms.Any(term =>
                        customer.Address.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        customer.CustomerName.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        customer.Nationality.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                if (!filteredCustomers.Any())
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "No customers found with the given search terms");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Customer search success", filteredCustomers);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
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
                    DoB = createCustomer.DoB,
                    Ocupation = createCustomer.Ocupation,
                    Nationality = createCustomer.Nationality,
                    Password = createCustomer.Password,
                    Role = "Customer"
                    
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
                customer.DoB = updateCustomer.DoB;
                customer.Ocupation = updateCustomer.Ocupation;
                customer.Password = updateCustomer.Password;
                 await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                return new JewelryAuction(Const.SUCCESS_GET, "Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }


    }
    


}
