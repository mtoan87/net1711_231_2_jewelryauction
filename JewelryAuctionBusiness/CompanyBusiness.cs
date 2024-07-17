using Common;
using JewelryAuctionData;

using JewelryAuctionData.DTO;
using JewelryAuctionData.DTO.Company;
using JewelryAuctionData.DTO.Customer;
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
        private readonly UnitOfWork _unitOfWork;


        public CompanyBusiness()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<JewelryAuctionResult> GetAll()
        {
            try
            {
                var company = await _unitOfWork.CompanyRepository.GetAllAsync();
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
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(code);
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
        public async Task<JewelryAuctionResult> GetPaged(int pageNumber, int pageSize)
        {
            try
            {
                var bids = await _unitOfWork.CompanyRepository.GetPagedAsync(pageNumber, pageSize);
                var totalRecords = await _unitOfWork.CompanyRepository.CountAsync();

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
                // Split the search string by commas to get individual search criteria
                var searchTerms = search.Split(',');
                var allCustomers = await _unitOfWork.CompanyRepository.GetAllAsync();
                // Filter companies based on multiple search criteria
                var filteredCustomers = allCustomers.Where(customer =>
                    searchTerms.Any(term =>
                        customer.Address.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        customer.CompanyName.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        customer.Email.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase)))
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
        public async Task<JewelryAuctionResult> CreateCompany(CreateCompanyDTO createCompany)
        {
            try
            {
                var newCompany = new Company
                {
                    
                    CompanyName = createCompany.CompanyName,
                    Address = createCompany.Address,
                    Description = createCompany.Description,
                    Email = createCompany.Email,
                    EstablishmentDate = createCompany.EstablishmentDate,
                    Location = createCompany.Location,
                    NumberOfEmployees = createCompany.NumberOfEmployees,   
                    Industry = createCompany.Industry,
                    PhoneNumber = createCompany.PhoneNumber,
                };
                var company = await _unitOfWork.CompanyRepository.CreateAsync(newCompany);
                if (company == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, " Error!");
                }
                else
                {
                    return new JewelryAuction(Const.SUCCESS_GET, "Creat success!", company);
                }
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> UpdateCompany(UpdateCompanyDTO updateCompany)
        {
            try
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(updateCompany.CompanyId);
                if (company == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Company not found.");
                }

                company.CompanyName = updateCompany.CompanyName;
                company.Address = updateCompany.Address;
                company.Email = updateCompany.Email;
                company.Description = updateCompany.Description;
                company.PhoneNumber = updateCompany.PhoneNumber;
                company.EstablishmentDate = updateCompany.EstablishmentDate;
                company.Location = updateCompany.Location;
                company.NumberOfEmployees = updateCompany.NumberOfEmployees;
                company.Industry = updateCompany.Industry;

                _unitOfWork.CompanyRepository.UpdateAsync(company);
                return new JewelryAuction(Const.SUCCESS_GET, "Company updated successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<JewelryAuctionResult> DeleteCompany(int companyId)
        {
            try
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
                if (company == null)
                {
                    return new JewelryAuction(Const.WARINING_NO_DATA, "Company not found.");
                }

                await _unitOfWork.CompanyRepository.RemoveAsync(company);
                return new JewelryAuction(Const.SUCCESS_GET, "Company deleted successfully.");
            }
            catch (Exception ex)
            {
                return new JewelryAuction(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }

}
