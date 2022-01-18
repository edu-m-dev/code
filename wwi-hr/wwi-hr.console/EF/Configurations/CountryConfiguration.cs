﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using wwi.hr.EF;


namespace wwi.hr.EF.Configurations
{
    public partial class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.ToTable("Countries", "Application");

            entity.HasIndex(e => e.CountryName, "UQ_Application_Countries_CountryName")
                .IsUnique();

            entity.HasIndex(e => e.FormalName, "UQ_Application_Countries_FormalName")
                .IsUnique();

            entity.Property(e => e.CountryId)
                .HasColumnName("CountryID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[CountryID])");

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