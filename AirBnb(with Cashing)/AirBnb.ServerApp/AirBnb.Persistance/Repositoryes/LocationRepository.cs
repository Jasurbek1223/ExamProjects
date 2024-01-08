using AirBnb.Domain.Entities;
using AirBnb.Persistence.Caching;
using AirBnb.Persistence.DbContexts;
using AirBnb.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnb.Persistence.Repositories;

public class LocationRepository(
    LocationsDbContext dbContext,
    ICacheBroker cacheBroker)
    : EntityRepositoryBase<Location, LocationsDbContext>(dbContext, cacheBroker), ILocationRepository
{
    public new ValueTask<Location> CreateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        location.Id = Guid.Empty;
        return base.CreateAsync(location, saveChanges, cancellationToken);
    }

    public new ValueTask<Location?> DeleteAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(location, saveChanges, cancellationToken);
    }

    public new ValueTask<Location?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }

    public new IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = null, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<Location?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public new ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        var found = dbContext.Locations.SingleOrDefault(dbLocation => dbLocation.Id == location.Id)
                    ?? throw new InvalidOperationException("Location not found with this Id");

        found.PlaceName = location.PlaceName;
        found.AwayInKm = location.AwayInKm;
        found.Days = location.Days;
        found.Price = location.Price;
        found.Rate = location.Rate;
        found.ImageUrl = location.ImageUrl;

        return base.UpdateAsync(found, saveChanges, cancellationToken);
    }
}