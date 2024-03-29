﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Countries", "Application");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Countries_Archive", "Application");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.CountryName, "UQ_Application_Countries_CountryName")
                .IsUnique();

            entity.HasIndex(e => e.FormalName, "UQ_Application_Countries_FormalName")
                .IsUnique();

            entity.Property(e => e.CountryID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CountryID])");

            entity.Property(e => e.Continent)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(60);

            entity.Property(e => e.CountryType).HasMaxLength(20);

            entity.Property(e => e.FormalName)
                .IsRequired()
                .HasMaxLength(60);

            entity.Property(e => e.IsoAlpha3Code).HasMaxLength(3);

            entity.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.Subregion)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.Countries)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_Countries_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Country> entity);
    }
}
