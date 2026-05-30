using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ViajaBrasil.Domain.Entities;
using ViajaBrasil.Infrastructure.Seeds;

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
        
        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.CityIbgeCode)
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.City)
            .WithMany(x => x.TouristSpots)
            .HasForeignKey(x => x.CityIbgeCode)
            .HasPrincipalKey(x => x.IbgeCode)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(TouristSpotsSeed.Data);
    }
}