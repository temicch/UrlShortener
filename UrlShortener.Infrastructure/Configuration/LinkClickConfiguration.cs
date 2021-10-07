using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Configuration
{
    public class LinkClickConfiguration : IEntityTypeConfiguration<LinkClick>
    {
        public void Configure(EntityTypeBuilder<LinkClick> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(128)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Link)
                .WithMany()
                .HasForeignKey(x => x.LinkId)
                .IsRequired();
        }
    }
}