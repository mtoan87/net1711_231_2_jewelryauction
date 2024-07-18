using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JewelryAuctionData.Models;

public partial class Net17112312JewelryAuctionContext : DbContext
{
    public Net17112312JewelryAuctionContext()
    {
    }

    public Net17112312JewelryAuctionContext(DbContextOptions<Net17112312JewelryAuctionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuctionResult> AuctionResults { get; set; }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Jewelry> Jewelries { get; set; }

    public virtual DbSet<JoinAuction> JoinAuctions { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RequestAuction> RequestAuctions { get; set; }

    public virtual DbSet<RequestAuctionDetail> RequestAuctionDetails { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DBDefault"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuctionResult>(entity =>
        {
            entity.ToTable("AuctionResult");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Detail).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Bid).WithMany(p => p.AuctionResults)
                .HasForeignKey(d => d.BidId)
                .HasConstraintName("FK_AuctionResult_Bid");

            entity.HasOne(d => d.Customer).WithMany(p => p.AuctionResults)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_AuctionResult_Customer");

            entity.HasOne(d => d.Jewelry).WithMany(p => p.AuctionResults)
                .HasForeignKey(d => d.JewelryId)
                .HasConstraintName("FK_AuctionResult_Jewelry");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.ToTable("Bid");

            entity.Property(e => e.BidStatus).HasMaxLength(100);
            entity.Property(e => e.BidderName).HasMaxLength(100);
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.IsWining).HasMaxLength(100);
            entity.Property(e => e.JoinAuctionDescription).HasMaxLength(100);
            entity.Property(e => e.JoinAuctionName).HasMaxLength(100);

            entity.HasOne(d => d.Jewelry).WithMany(p => p.Bids)
                .HasForeignKey(d => d.JewelryId)
                .HasConstraintName("FK_Bid_Jewelry");

            entity.HasOne(d => d.JoinAuction).WithMany(p => p.Bids)
                .HasForeignKey(d => d.JoinAuctionId)
                .HasConstraintName("FK_Bid_JoinAuction");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EstablishmentDate).HasColumnType("datetime");
            entity.Property(e => e.Industry).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.DoB).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.Ocupation).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(100);
        });

        modelBuilder.Entity<Jewelry>(entity =>
        {
            entity.ToTable("Jewelry");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.JewelryName).HasMaxLength(50);
            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.Quantitative).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Weight).HasMaxLength(50);
        });

        modelBuilder.Entity<JoinAuction>(entity =>
        {
            entity.ToTable("JoinAuction");

            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.Host).HasMaxLength(50);
            entity.Property(e => e.IsLive).HasMaxLength(50);
            entity.Property(e => e.JoinAuctionDescription).HasMaxLength(200);
            entity.Property(e => e.JoinAuctionName).HasMaxLength(100);
            entity.Property(e => e.JoinAuctionStatus).HasMaxLength(100);
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.JoinAuctions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_JoinAuction_Customer");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.AuctionResult).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AuctionResultId)
                .HasConstraintName("FK_Payment_AuctionResult");
        });

        modelBuilder.Entity<RequestAuction>(entity =>
        {
            entity.ToTable("RequestAuction");

            entity.Property(e => e.ApprovedAt).HasColumnType("datetime");
            entity.Property(e => e.AuctionName).HasMaxLength(50);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.FinalEstimateSentAt).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.RequestAuctions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_RequestAuction_Customer");
        });

        modelBuilder.Entity<RequestAuctionDetail>(entity =>
        {
            entity.ToTable("RequestAuctionDetail");

            entity.Property(e => e.ApprovedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.JewelryDescription).HasMaxLength(255);
            entity.Property(e => e.JewelryName).HasMaxLength(100);
            entity.Property(e => e.ReceivedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Jewelry).WithMany(p => p.RequestAuctionDetails)
                .HasForeignKey(d => d.JewelryId)
                .HasConstraintName("FK_RequestAuctionDetail_Jewelry");

            entity.HasOne(d => d.RequestAuction).WithMany(p => p.RequestAuctionDetails)
                .HasForeignKey(d => d.RequestAuctionId)
                .HasConstraintName("FK_RequestAuctionDetail_RequestAuction");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
