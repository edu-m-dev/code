﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {
            entity.ToTable("Suppliers", "Purchasing");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Suppliers_Archive", "Purchasing");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.AlternateContactPersonID, "FK_Purchasing_Suppliers_AlternateContactPersonID");

            entity.HasIndex(e => e.DeliveryCityID, "FK_Purchasing_Suppliers_DeliveryCityID");

            entity.HasIndex(e => e.DeliveryMethodID, "FK_Purchasing_Suppliers_DeliveryMethodID");

            entity.HasIndex(e => e.PostalCityID, "FK_Purchasing_Suppliers_PostalCityID");

            entity.HasIndex(e => e.PrimaryContactPersonID, "FK_Purchasing_Suppliers_PrimaryContactPersonID");

            entity.HasIndex(e => e.SupplierCategoryID, "FK_Purchasing_Suppliers_SupplierCategoryID");

            entity.HasIndex(e => e.SupplierName, "UQ_Purchasing_Suppliers_SupplierName")
                .IsUnique();

            entity.Property(e => e.SupplierID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[SupplierID])");

            entity.Property(e => e.BankAccountBranch).HasMaxLength(50);

            entity.Property(e => e.BankAccountCode).HasMaxLength(20);

            entity.Property(e => e.BankAccountName).HasMaxLength(50);

            entity.Property(e => e.BankAccountNumber).HasMaxLength(20);

            entity.Property(e => e.BankInternationalCode).HasMaxLength(20);

            entity.Property(e => e.DeliveryAddressLine1)
                .IsRequired()
                .HasMaxLength(60);

            entity.Property(e => e.DeliveryAddressLine2).HasMaxLength(60);

            entity.Property(e => e.DeliveryPostalCode)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.FaxNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.PostalAddressLine1)
                .IsRequired()
                .HasMaxLength(60);

            entity.Property(e => e.PostalAddressLine2).HasMaxLength(60);

            entity.Property(e => e.PostalPostalCode)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.SupplierName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.SupplierReference).HasMaxLength(20);

            entity.Property(e => e.WebsiteURL)
                .IsRequired()
                .HasMaxLength(256);

            entity.HasOne(d => d.AlternateContactPerson)
                .WithMany(p => p.SupplierAlternateContactPeople)
                .HasForeignKey(d => d.AlternateContactPersonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_AlternateContactPersonID_Application_People");

            entity.HasOne(d => d.DeliveryCity)
                .WithMany(p => p.SupplierDeliveryCities)
                .HasForeignKey(d => d.DeliveryCityID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_DeliveryCityID_Application_Cities");

            entity.HasOne(d => d.DeliveryMethod)
                .WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.DeliveryMethodID)
                .HasConstraintName("FK_Purchasing_Suppliers_DeliveryMethodID_Application_DeliveryMethods");

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.SupplierLastEditedByNavigations)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_Application_People");

            entity.HasOne(d => d.PostalCity)
                .WithMany(p => p.SupplierPostalCities)
                .HasForeignKey(d => d.PostalCityID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_PostalCityID_Application_Cities");

            entity.HasOne(d => d.PrimaryContactPerson)
                .WithMany(p => p.SupplierPrimaryContactPeople)
                .HasForeignKey(d => d.PrimaryContactPersonID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_PrimaryContactPersonID_Application_People");

            entity.HasOne(d => d.SupplierCategory)
                .WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.SupplierCategoryID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Purchasing_Suppliers_SupplierCategoryID_Purchasing_SupplierCategories");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Supplier> entity);
    }
}
