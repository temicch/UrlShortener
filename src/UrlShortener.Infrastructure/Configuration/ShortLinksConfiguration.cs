using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Configuration;

public class ShortLinksConfiguration : IEntityTypeConfiguration<ShortLink>
{
    public void Configure(EntityTypeBuilder<ShortLink> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(128)
            .ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Alias)
            .IsUnique();
    }
}