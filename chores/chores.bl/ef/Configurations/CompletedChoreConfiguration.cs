﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using chores.bl.ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace chores.bl.ef.Configurations
{
    public partial class CompletedChoreConfiguration : IEntityTypeConfiguration<CompletedChore>
    {
        public void Configure(EntityTypeBuilder<CompletedChore> entity)
        {
            entity.ToTable("completed_chores");

            entity.HasIndex(e => e.Id, "IX_completed_chores_Id")
                .IsUnique();

            entity.HasOne(d => d.Chore)
                .WithMany(p => p.CompletedChores)
                .HasForeignKey(d => d.ChoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CompletedChore> entity);
    }
}
