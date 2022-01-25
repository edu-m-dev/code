﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class CustomerCategoryConfiguration : IEntityTypeConfiguration<CustomerCategory>
    {
        public void Configure(EntityTypeBuilder<CustomerCategory> entity)
        {
            entity.ToTable("CustomerCategories", "Sales");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("CustomerCategories_Archive", "Sales");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.CustomerCategoryName, "UQ_Sales_CustomerCategories_CustomerCategoryName")
                .IsUnique();

            entity.Property(e => e.CustomerCategoryId)
                .HasColumnName("CustomerCategoryID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CustomerCategoryID])");

            entity.Property(e => e.CustomerCategoryName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.CustomerCategories)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_CustomerCategories_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CustomerCategory> entity);
    }
}
