﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class SpecialDealConfiguration : IEntityTypeConfiguration<SpecialDeal>
    {
        public void Configure(EntityTypeBuilder<SpecialDeal> entity)
        {
            entity.ToTable("SpecialDeals", "Sales");

            entity.HasIndex(e => e.BuyingGroupID, "FK_Sales_SpecialDeals_BuyingGroupID");

            entity.HasIndex(e => e.CustomerCategoryID, "FK_Sales_SpecialDeals_CustomerCategoryID");

            entity.HasIndex(e => e.CustomerID, "FK_Sales_SpecialDeals_CustomerID");

            entity.HasIndex(e => e.StockGroupID, "FK_Sales_SpecialDeals_StockGroupID");

            entity.HasIndex(e => e.StockItemID, "FK_Sales_SpecialDeals_StockItemID");

            entity.Property(e => e.SpecialDealID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[SpecialDealID])");

            entity.Property(e => e.DealDescription)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");

            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(18, 3)");

            entity.Property(e => e.EndDate).HasColumnType("date");

            entity.Property(e => e.LastEditedWhen).HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.BuyingGroup)
                .WithMany(p => p.SpecialDeals)
                .HasForeignKey(d => d.BuyingGroupID)
                .HasConstraintName("FK_Sales_SpecialDeals_BuyingGroupID_Sales_BuyingGroups");

            entity.HasOne(d => d.CustomerCategory)
                .WithMany(p => p.SpecialDeals)
                .HasForeignKey(d => d.CustomerCategoryID)
                .HasConstraintName("FK_Sales_SpecialDeals_CustomerCategoryID_Sales_CustomerCategories");

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.SpecialDeals)
                .HasForeignKey(d => d.CustomerID)
                .HasConstraintName("FK_Sales_SpecialDeals_CustomerID_Sales_Customers");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.SpecialDeals)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_SpecialDeals_Application_People");

            entity.HasOne(d => d.StockGroup)
                .WithMany(p => p.SpecialDeals)
                .HasForeignKey(d => d.StockGroupID)
                .HasConstraintName("FK_Sales_SpecialDeals_StockGroupID_Warehouse_StockGroups");

            entity.HasOne(d => d.StockItem)
                .WithMany(p => p.SpecialDeals)
                .HasForeignKey(d => d.StockItemID)
                .HasConstraintName("FK_Sales_SpecialDeals_StockItemID_Warehouse_StockItems");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SpecialDeal> entity);
    }
}
