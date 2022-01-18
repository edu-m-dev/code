﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using wwi.hr.EF;


namespace wwi.hr.EF.Configurations
{
    public partial class PeopleConfiguration : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> entity)
        {
            entity.HasKey(e => e.PersonId)
                .HasName("PK_Application_People");

            entity.ToTable("People", "Application");

            entity.HasIndex(e => e.FullName, "IX_Application_People_FullName");

            entity.HasIndex(e => e.IsEmployee, "IX_Application_People_IsEmployee");

            entity.HasIndex(e => e.IsSalesperson, "IX_Application_People_IsSalesperson");

            entity.HasIndex(e => new { e.IsPermittedToLogon, e.PersonId }, "IX_Application_People_Perf_20160301_05");

            entity.Property(e => e.PersonId)
                .HasColumnName("PersonID")
                .HasDefaultValueSql("(NEXT VALUE FOR [Sequences].[PersonID])");

            entity.Property(e => e.EmailAddress).HasMaxLength(256);

            entity.Property(e => e.FaxNumber).HasMaxLength(20);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LogonName).HasMaxLength(50);

            entity.Property(e => e.OtherLanguages).HasComputedColumnSql("(json_query([CustomFields],N'$.OtherLanguages'))", false);

            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.Property(e => e.PreferredName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.SearchName)
                .IsRequired()
                .HasMaxLength(101)
                .HasComputedColumnSql("(concat([PreferredName],N' ',[FullName]))", true);

            entity.HasOne(d => d.LastEditedByNavigation)
                .WithMany(p => p.InverseLastEditedByNavigation)
                .HasForeignKey(d => d.LastEditedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Application_People_Application_People");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<People> entity);
    }
}
