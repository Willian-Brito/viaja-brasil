using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Infrastructure.Configurations;

public class TouristSpotConfiguration : IEntityTypeConfiguration<TouristSpot>
{
    public void Configure(EntityTypeBuilder<TouristSpot> builder)
    {
        builder.ToTable("TouristSpots");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Location)
                .HasColumnName("Location")
                .HasMaxLength(200)
                .IsRequired();

            address.Property(x => x.City)
                .HasColumnName("City")
                .HasMaxLength(100)
                .IsRequired();

            address.Property(x => x.State)
                .HasColumnName("State")
                .HasConversion<string>()
                .HasMaxLength(2)
                .IsRequired();
        });
    }
}