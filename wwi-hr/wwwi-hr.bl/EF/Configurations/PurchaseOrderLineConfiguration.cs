﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderLine> entity)
        {
            entity.ToTable("PurchaseOrderLines", "Purchasing");

            entity.HasIndex(e => e.PackageTypeId, "FK_Purchasing_PurchaseOrderLines_PackageTypeID");

            entity.HasIndex(e => e.PurchaseOrderId, "FK_Purchasing_PurchaseOrderLines_PurchaseOrderID");

            entity.HasIndex(e => e.StockItemId, "FK_Purchasing_PurchaseOrderLines_StockItemID");

            entity.HasIndex(e => new { e.IsOrderLineFinalized, e.StockItemId }, "IX_Purchasing_PurchaseOrderLines_Perf_20160301_4");

            entity.Property(e => e.PurchaseOrderLineId)
                .HasColumnName("PurchaseOrderLineID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PurchaseOrderLineID])");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.ExpectedUnitPricePerOuter).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.LastReceiptDate).HasColumnType("date");

            entity.Property(e => e.PackageTypeId).HasColumnName("PackageTypeID");

            entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

            entity.Property(e => e.StockItemId).HasColumnName("StockItemID");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_Application_People");

            entity.HasOne(d => d.PackageType)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(d => d.PackageTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_PackageTypeID_Warehouse_PackageTypes");

            entity.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_PurchaseOrderID_Purchasing_PurchaseOrders");

            entity.HasOne(d => d.StockItem)
                .WithMany(p => p.PurchaseOrderLines)
                .HasForeignKey(d => d.StockItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_PurchaseOrderLines_StockItemID_Warehouse_StockItems");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PurchaseOrderLine> entity);
    }
}