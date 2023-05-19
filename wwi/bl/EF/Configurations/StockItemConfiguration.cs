﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> entity)
        {
            entity.ToTable("StockItems", "Warehouse");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("StockItems_Archive", "Warehouse");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.ColorID, "FK_Warehouse_StockItems_ColorID");

            entity.HasIndex(e => e.OuterPackageID, "FK_Warehouse_StockItems_OuterPackageID");

            entity.HasIndex(e => e.SupplierID, "FK_Warehouse_StockItems_SupplierID");

            entity.HasIndex(e => e.UnitPackageID, "FK_Warehouse_StockItems_UnitPackageID");

            entity.HasIndex(e => e.StockItemName, "UQ_Warehouse_StockItems_StockItemName")
                .IsUnique();

            entity.Property(e => e.StockItemID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StockItemID])");

            entity.Property(e => e.Barcode).HasMaxLength(50);

            entity.Property(e => e.Brand).HasMaxLength(50);

            entity.Property(e => e.RecommendedRetailPrice).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.SearchDetails)
                .IsRequired()
                .HasComputedColumnSql("(concat([StockItemName],N' ',[MarketingComments]))", false);

            entity.Property(e => e.Size).HasMaxLength(20);

            entity.Property(e => e.StockItemName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Tags).HasComputedColumnSql("(json_query([CustomFields],N'$.Tags'))", false);

            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 3)");

            entity.Property(e => e.TypicalWeightPerUnit).HasColumnType("decimal(18, 3)");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Color)
                .WithMany(p => p.StockItems)
                .HasForeignKey(d => d.ColorID)
                .HasConstraintName("FK_Warehouse_StockItems_ColorID_Warehouse_Colors");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.StockItems)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_Application_People");

            entity.HasOne(d => d.OuterPackage)
                .WithMany(p => p.StockItemOuterPackages)
                .HasForeignKey(d => d.OuterPackageID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_OuterPackageID_Warehouse_PackageTypes");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.StockItems)
                .HasForeignKey(d => d.SupplierID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_SupplierID_Purchasing_Suppliers");

            entity.HasOne(d => d.UnitPackage)
                .WithMany(p => p.StockItemUnitPackages)
                .HasForeignKey(d => d.UnitPackageID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockItems_UnitPackageID_Warehouse_PackageTypes");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StockItem> entity);
    }
}