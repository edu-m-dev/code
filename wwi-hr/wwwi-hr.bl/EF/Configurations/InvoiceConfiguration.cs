﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> entity)
        {
            entity.ToTable("Invoices", "Sales");

            entity.HasIndex(e => e.AccountsPersonId, "FK_Sales_Invoices_AccountsPersonID");

            entity.HasIndex(e => e.BillToCustomerId, "FK_Sales_Invoices_BillToCustomerID");

            entity.HasIndex(e => e.ContactPersonId, "FK_Sales_Invoices_ContactPersonID");

            entity.HasIndex(e => e.CustomerId, "FK_Sales_Invoices_CustomerID");

            entity.HasIndex(e => e.DeliveryMethodId, "FK_Sales_Invoices_DeliveryMethodID");

            entity.HasIndex(e => e.OrderId, "FK_Sales_Invoices_OrderID");

            entity.HasIndex(e => e.PackedByPersonId, "FK_Sales_Invoices_PackedByPersonID");

            entity.HasIndex(e => e.SalespersonPersonId, "FK_Sales_Invoices_SalespersonPersonID");

            entity.HasIndex(e => e.ConfirmedDeliveryTime, "IX_Sales_Invoices_ConfirmedDeliveryTime");

            entity.Property(e => e.InvoiceId)
                .HasColumnName("InvoiceID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[InvoiceID])");

            entity.Property(e => e.AccountsPersonId).HasColumnName("AccountsPersonID");

            entity.Property(e => e.BillToCustomerId).HasColumnName("BillToCustomerID");

            entity.Property(e => e.ConfirmedDeliveryTime).HasComputedColumnSql("(TRY_CONVERT([datetime2](7),json_value([ReturnedDeliveryData],N'$.DeliveredWhen'),(126)))", false);

            entity.Property(e => e.ConfirmedReceivedBy)
                .HasMaxLength(4000)
                .HasComputedColumnSql("(json_value([ReturnedDeliveryData],N'$.ReceivedBy'))", false);

            entity.Property(e => e.ContactPersonId).HasColumnName("ContactPersonID");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.Property(e => e.CustomerPurchaseOrderNumber).HasMaxLength(20);

            entity.Property(e => e.DeliveryMethodId).HasColumnName("DeliveryMethodID");

            entity.Property(e => e.DeliveryRun).HasMaxLength(5);

            entity.Property(e => e.InvoiceDate).HasColumnType("date");

            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");

            entity.Property(e => e.PackedByPersonId).HasColumnName("PackedByPersonID");

            entity.Property(e => e.RunPosition).HasMaxLength(5);

            entity.Property(e => e.SalespersonPersonId).HasColumnName("SalespersonPersonID");

            entity.HasOne(d => d.AccountsPerson)
                .WithMany(p => p.InvoiceAccountsPeople)
                .HasForeignKey(d => d.AccountsPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_AccountsPersonID_Application_People");

            entity.HasOne(d => d.BillToCustomer)
                .WithMany(p => p.InvoiceBillToCustomers)
                .HasForeignKey(d => d.BillToCustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_BillToCustomerID_Sales_Customers");

            entity.HasOne(d => d.ContactPerson)
                .WithMany(p => p.InvoiceContactPeople)
                .HasForeignKey(d => d.ContactPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_ContactPersonID_Application_People");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.InvoiceCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_CustomerID_Sales_Customers");

            entity.HasOne(d => d.DeliveryMethod)
                .WithMany(p => p.Invoices)
                .HasForeignKey(d => d.DeliveryMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_DeliveryMethodID_Application_DeliveryMethods");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.InvoiceLastEditedByNavigations)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_Application_People");

            entity.HasOne(d => d.Order)
                .WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Sales_Invoices_OrderID_Sales_Orders");

            entity.HasOne(d => d.PackedByPerson)
                .WithMany(p => p.InvoicePackedByPeople)
                .HasForeignKey(d => d.PackedByPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_PackedByPersonID_Application_People");

            entity.HasOne(d => d.SalespersonPerson)
                .WithMany(p => p.InvoiceSalespersonPeople)
                .HasForeignKey(d => d.SalespersonPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Invoices_SalespersonPersonID_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Invoice> entity);
    }
}
