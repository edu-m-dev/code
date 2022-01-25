﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using wwi.bl.EF.Configurations;
#nullable disable

namespace wwi.bl.EF
{
    public partial class WwiContext : DbContext
    {
        public WwiContext()
        {
        }

        public WwiContext(DbContextOptions<WwiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BuyingGroup> BuyingGroups { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ColdRoomTemperature> ColdRoomTemperatures { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Customer1> Customers1 { get; set; }
        public virtual DbSet<CustomerCategory> CustomerCategories { get; set; }
        public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<PackageType> PackageTypes { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public virtual DbSet<SpecialDeal> SpecialDeals { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<StockGroup> StockGroups { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }
        public virtual DbSet<StockItemHolding> StockItemHoldings { get; set; }
        public virtual DbSet<StockItemStockGroup> StockItemStockGroups { get; set; }
        public virtual DbSet<StockItemTransaction> StockItemTransactions { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Supplier1> Suppliers1 { get; set; }
        public virtual DbSet<SupplierCategory> SupplierCategories { get; set; }
        public virtual DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        public virtual DbSet<SystemParameter> SystemParameters { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<VehicleTemperature> VehicleTemperatures { get; set; }
        public virtual DbSet<VehicleTemperature1> VehicleTemperatures1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\dev;Initial Catalog=WideWorldImporters;Persist Security Info=True;User ID=sa;Password=9ext945A%$");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_100_CI_AS");

            modelBuilder.ApplyConfiguration(new Configurations.BuyingGroupConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CityConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ColdRoomTemperatureConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ColorConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CountryConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Customer1Configuration());
            modelBuilder.ApplyConfiguration(new Configurations.CustomerCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CustomerTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.DeliveryMethodConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.InvoiceLineConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OrderConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.OrderLineConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PackageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PaymentMethodConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PersonConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PurchaseOrderLineConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SpecialDealConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StateProvinceConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StockGroupConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StockItemConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StockItemHoldingConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StockItemStockGroupConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.StockItemTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Supplier1Configuration());
            modelBuilder.ApplyConfiguration(new Configurations.SupplierCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SupplierTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SystemParameterConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TransactionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VehicleTemperatureConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VehicleTemperature1Configuration());
            modelBuilder.HasSequence<int>("BuyingGroupID", "Sequences").StartsAt(3);

            modelBuilder.HasSequence<int>("CityID", "Sequences").StartsAt(38187);

            modelBuilder.HasSequence<int>("ColorID", "Sequences").StartsAt(37);

            modelBuilder.HasSequence<int>("CountryID", "Sequences").StartsAt(242);

            modelBuilder.HasSequence<int>("CustomerCategoryID", "Sequences").StartsAt(9);

            modelBuilder.HasSequence<int>("CustomerID", "Sequences").StartsAt(1062);

            modelBuilder.HasSequence<int>("DeliveryMethodID", "Sequences").StartsAt(11);

            modelBuilder.HasSequence<int>("InvoiceID", "Sequences").StartsAt(70511);

            modelBuilder.HasSequence<int>("InvoiceLineID", "Sequences").StartsAt(228266);

            modelBuilder.HasSequence<int>("OrderID", "Sequences").StartsAt(73596);

            modelBuilder.HasSequence<int>("OrderLineID", "Sequences").StartsAt(231413);

            modelBuilder.HasSequence<int>("PackageTypeID", "Sequences").StartsAt(15);

            modelBuilder.HasSequence<int>("PaymentMethodID", "Sequences").StartsAt(5);

            modelBuilder.HasSequence<int>("PersonID", "Sequences").StartsAt(3262);

            modelBuilder.HasSequence<int>("PurchaseOrderID", "Sequences").StartsAt(2075);

            modelBuilder.HasSequence<int>("PurchaseOrderLineID", "Sequences").StartsAt(8368);

            modelBuilder.HasSequence<int>("SpecialDealID", "Sequences").StartsAt(3);

            modelBuilder.HasSequence<int>("StateProvinceID", "Sequences").StartsAt(54);

            modelBuilder.HasSequence<int>("StockGroupID", "Sequences").StartsAt(11);

            modelBuilder.HasSequence<int>("StockItemID", "Sequences").StartsAt(228);

            modelBuilder.HasSequence<int>("StockItemStockGroupID", "Sequences").StartsAt(443);

            modelBuilder.HasSequence<int>("SupplierCategoryID", "Sequences").StartsAt(10);

            modelBuilder.HasSequence<int>("SupplierID", "Sequences").StartsAt(14);

            modelBuilder.HasSequence<int>("SystemParameterID", "Sequences").StartsAt(2);

            modelBuilder.HasSequence<int>("TransactionID", "Sequences").StartsAt(336253);

            modelBuilder.HasSequence<int>("TransactionTypeID", "Sequences").StartsAt(14);

            OnModelCreatingGeneratedFunctions(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
