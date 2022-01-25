﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> entity)
        {
            entity.ToTable("InvoiceLines", "Sales");

            entity.HasIndex(e => e.InvoiceId, "FK_Sales_InvoiceLines_InvoiceID");

            entity.HasIndex(e => e.PackageTypeId, "FK_Sales_InvoiceLines_PackageTypeID");

            entity.HasIndex(e => e.StockItemId, "FK_Sales_InvoiceLines_StockItemID");

            entity.Property(e => e.InvoiceLineId)
                .HasColumnName("InvoiceLineID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[InvoiceLineID])");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ExtendedPrice).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.LineProfit).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.PackageTypeId).HasColumnName("PackageTypeID");

            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");

            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 3)");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Invoice)
                .WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_InvoiceID_Sales_Invoices");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_Application_People");

            entity.HasOne(d => d.PackageType)
                .WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.PackageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_PackageTypeID_Warehouse_PackageTypes");

            entity.HasOne(d => d.StockItem)
                .WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.StockItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_InvoiceLines_StockItemID_Warehouse_StockItems");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<InvoiceLine> entity);
    }
}
