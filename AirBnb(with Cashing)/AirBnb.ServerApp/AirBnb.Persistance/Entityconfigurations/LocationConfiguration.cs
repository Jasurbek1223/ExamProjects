using AirBnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirBnb.Persistance.Entityconfigurations;

public class LocationConfiguration
    : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder
            .HasOne(location => location.Category)
            .WithMany(category => category.Locations)
            .HasForeignKey(location => location.CategoryId);

    }
}