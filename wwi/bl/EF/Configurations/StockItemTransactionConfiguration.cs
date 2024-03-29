﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class StockItemTransactionConfiguration : IEntityTypeConfiguration<StockItemTransaction>
    {
        public void Configure(EntityTypeBuilder<StockItemTransaction> entity)
        {
            entity.HasKey(e => e.StockItemTransactionID)
                .HasName("PK_Warehouse_StockItemTransactions")
                .IsClustered(false);

            entity.ToTable("StockItemTransactions", "Warehouse");

            entity.HasIndex(e => e.CustomerID, "FK_Warehouse_StockItemTransactions_CustomerID");

            entity.HasIndex(e => e.InvoiceID, "FK_Warehouse_StockItemTransactions_InvoiceID");

            entity.HasIndex(e => e.PurchaseOrderID, "FK_Warehouse_StockItemTransactions_PurchaseOrderID");

            entity.HasIndex(e => e.StockItemID, "FK_Warehouse_StockItemTransactions_StockItemID");

            entity.HasIndex(e => e.SupplierID, "FK_Warehouse_StockItemTransactions_SupplierID");

            entity.HasIndex(e => e.TransactionTypeID, "FK_Warehouse_StockItemTransactions_TransactionTypeID");

            entity.Property(e => e.StockItemTransactionID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[TransactionID])");

            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.CustomerID)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_CustomerID_Sales_Customers");

            entity.HasOne(d => d.Invoice)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.InvoiceID)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_InvoiceID_Sales_Invoices");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_Application_People");

            entity.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.PurchaseOrderID)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_PurchaseOrderID_Purchasing_PurchaseOrders");

            entity.HasOne(d => d.StockItem)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.StockItemID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_StockItemID_Warehouse_StockItems");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.SupplierID)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_SupplierID_Purchasing_Suppliers");

            entity.HasOne(d => d.TransactionType)
                .WithMany(p => p.StockItemTransactions)
                .HasForeignKey(d => d.TransactionTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItemTransactions_TransactionTypeID_Application_TransactionTypes");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StockItemTransaction> entity);
    }
}
