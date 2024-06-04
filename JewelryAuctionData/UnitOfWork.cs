using JewelryAuctionData.Models;
using JewelryAuctionData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryAuctionData
{
    public class UnitOfWork
    {
        private Net17112312JewelryAuctionContext _context;

        private CustomerRepository _customerRepository;
        private CompanyRepository _companyRepository;
        private PaymentRepository _paymentRepository;
        public UnitOfWork() 
        {
            _context ??= new Net17112312JewelryAuctionContext();
        }

        
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ??= new Repository.CustomerRepository();
            }
        }
        public CompanyRepository CompanyRepository
        {
            get
            {
                return _companyRepository ??= new Repository.CompanyRepository();
            }
        }
        public PaymentRepository PaymentRepository
        {
            get
            {
                return _paymentRepository ??= new Repository.PaymentRepository(_context);
            }
        }
    }
}
