﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class StateProvinceConfiguration : IEntityTypeConfiguration<StateProvince>
    {
        public void Configure(EntityTypeBuilder<StateProvince> entity)
        {
            entity.ToTable("StateProvinces", "Application");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("StateProvinces_Archive", "Application");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.CountryId, "FK_Application_StateProvinces_CountryID");

            entity.HasIndex(e => e.SalesTerritory, "IX_Application_StateProvinces_SalesTerritory");

            entity.HasIndex(e => e.StateProvinceName, "UQ_Application_StateProvinces_StateProvinceName")
                .IsUnique();

            entity.Property(e => e.StateProvinceId)
                .HasColumnName("StateProvinceID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[StateProvinceID])");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");

            entity.Property(e => e.SalesTerritory)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.StateProvinceCode)
                .IsRequired()
                .HasMaxLength(5);

            entity.Property(e => e.StateProvinceName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Country)
                .WithMany(p => p.StateProvinces)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_StateProvinces_CountryID_Application_Countries");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.StateProvinces)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_StateProvinces_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<StateProvince> entity);
    }
}
