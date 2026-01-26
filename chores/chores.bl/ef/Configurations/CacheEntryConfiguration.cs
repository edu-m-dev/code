using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chores.bl.ef.Configurations
{
    public class CacheEntryConfiguration : IEntityTypeConfiguration<CacheEntry>
    {
        public void Configure(EntityTypeBuilder<CacheEntry> builder)
        {
            builder.ToTable("chores_cache");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasMaxLength(449)
                .HasColumnType("nvarchar(449)");

            builder.Property(e => e.Value)
                .HasColumnType("varbinary(max)")
                .IsRequired();

            builder.Property(e => e.ExpiresAtTime)
                .HasColumnType("datetimeoffset");

            builder.Property(e => e.SlidingExpirationInSeconds)
                .HasColumnType("bigint");

            builder.Property(e => e.AbsoluteExpiration)
                .HasColumnType("datetimeoffset");
        }
    }
}
