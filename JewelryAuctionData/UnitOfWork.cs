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

        private RequestAuctionRepository _requestAuction;
        private RequestAuctionDetailRepository _requestAuctionDetail;

        private PaymentRepository _paymentRepository;

        private JewelryRepository _jewelryRepository;
        private AuctionResultRepository _auctionResultRepository;

        private JoinAuctionRepository _joinAuctionRepository;
        private BidRepository _bidRepository;

        public UnitOfWork() 
        {
            _context ??= new Net17112312JewelryAuctionContext();
        }

        
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ??= new Repository.CustomerRepository(_context);
            }
        }
        public CompanyRepository CompanyRepository
        {
            get
            {
                return _companyRepository ??= new Repository.CompanyRepository(_context);
            }
        }
        public PaymentRepository PaymentRepository
        {
            get
            {
                return _paymentRepository ??= new Repository.PaymentRepository(_context);
            }
        }
        public JewelryRepository JewelryRepository
        {
            get
            {
                return _jewelryRepository ??= new Repository.JewelryRepository(_context);
            }
        }
        public AuctionResultRepository AuctionResultRepository
        {
            get
            {
                return _auctionResultRepository ??= new Repository.AuctionResultRepository(_context);
            }
        }
        public JoinAuctionRepository joinAuctionRepository
        {
            get
            {
                return _joinAuctionRepository ??= new Repository.JoinAuctionRepository(_context);
            }
        }

        public BidRepository bidRepository
        {
            get
            {
                return _bidRepository ??= new Repository.BidRepository(_context);
            }
        }

        public RequestAuctionRepository RequestAuctionRepository
        {
            get
            {
                return _requestAuction ??= new RequestAuctionRepository(_context);
            }
        }

        public RequestAuctionDetailRepository RequestAuctionDetailRepository
        {
            get
            {
                return _requestAuctionDetail ??= new RequestAuctionDetailRepository(_context);
            }
        }
    }
}
