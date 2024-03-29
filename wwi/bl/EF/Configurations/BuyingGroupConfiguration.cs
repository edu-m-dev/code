﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class BuyingGroupConfiguration : IEntityTypeConfiguration<BuyingGroup>
    {
        public void Configure(EntityTypeBuilder<BuyingGroup> entity)
        {
            entity.ToTable("BuyingGroups", "Sales");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("BuyingGroups_Archive", "Sales");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.BuyingGroupName, "UQ_Sales_BuyingGroups_BuyingGroupName")
                .IsUnique();

            entity.Property(e => e.BuyingGroupID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[BuyingGroupID])");

            entity.Property(e => e.BuyingGroupName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.BuyingGroups)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_BuyingGroups_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<BuyingGroup> entity);
    }
}
