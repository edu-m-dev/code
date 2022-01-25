﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class StockGroupConfiguration : IEntityTypeConfiguration<StockGroup>
    {
        public void Configure(EntityTypeBuilder<StockGroup> entity)
        {
            entity.ToTable("StockGroups", "Warehouse");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("StockGroups_Archive", "Warehouse");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.StockGroupName, "UQ_Warehouse_StockGroups_StockGroupName")
                .IsUnique();

            entity.Property(e => e.StockGroupID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StockGroupID])");

            entity.Property(e => e.StockGroupName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.StockGroups)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_StockGroups_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StockGroup> entity);
    }
}
