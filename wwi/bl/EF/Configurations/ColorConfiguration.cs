﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using wwi.bl.EF;

namespace wwi.bl.EF.Configurations
{
    public partial class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> entity)
        {
            entity.ToTable("Colors", "Warehouse");

            entity.ToTable(tb => tb.IsTemporal(ttb =>
    {
        ttb.UseHistoryTable("Colors_Archive", "Warehouse");
        ttb
            .HasPeriodStart("ValidFrom")
            .HasColumnName("ValidFrom");
        ttb
            .HasPeriodEnd("ValidTo")
            .HasColumnName("ValidTo");
    }
));

            entity.HasIndex(e => e.ColorName, "UQ_Warehouse_Colors_ColorName")
                .IsUnique();

            entity.Property(e => e.ColorID).HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[ColorID])");

            entity.Property(e => e.ColorName)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.Colors)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouse_Colors_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Color> entity);
    }
}
