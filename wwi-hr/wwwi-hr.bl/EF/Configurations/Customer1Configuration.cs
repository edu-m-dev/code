﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class Customer1Configuration : IEntityTypeConfiguration<Customer1>
    {
        public void Configure(EntityTypeBuilder<Customer1> entity)
        {
            entity.HasNoKey();

            entity.ToView("Customers", "Website");

            entity.Property(e => e.AlternateContact).HasMaxLength(50);

            entity.Property(e => e.BuyingGroupName).HasMaxLength(50);

            entity.Property(e => e.CityName).HasMaxLength(50);

            entity.Property(e => e.CustomerCategoryName).HasMaxLength(50);

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.DeliveryMethod).HasMaxLength(50);

            entity.Property(e => e.DeliveryRun).HasMaxLength(5);

            entity.Property(e => e.FaxNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.PrimaryContact).HasMaxLength(50);

            entity.Property(e => e.RunPosition).HasMaxLength(5);

            entity.Property(e => e.WebsiteUrl)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("WebsiteURL");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Customer1> entity);
    }
}
