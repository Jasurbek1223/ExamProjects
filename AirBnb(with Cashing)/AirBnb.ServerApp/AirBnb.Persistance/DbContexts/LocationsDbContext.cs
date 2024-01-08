using AirBnb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Persistence.DbContexts;

public class LocationsDbContext : DbContext
{
    public DbSet<Location> Locations => Set<Location>();

    public DbSet<LocationCategory> LocationCategories => Set<LocationCategory>();

    public LocationsDbContext(DbContextOptions<LocationsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationsDbContext).Assembly);
    }
}