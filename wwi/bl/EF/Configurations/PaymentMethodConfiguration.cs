﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> entity)
        {
            entity.ToTable("PaymentMethods", "Application");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("PaymentMethods_Archive", "Application");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.PaymentMethodName, "UQ_Application_PaymentMethods_PaymentMethodName")
                .IsUnique();

            entity.Property(e => e.PaymentMethodID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PaymentMethodID])");

            entity.Property(e => e.PaymentMethodName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_PaymentMethods_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<PaymentMethod> entity);
    }
}