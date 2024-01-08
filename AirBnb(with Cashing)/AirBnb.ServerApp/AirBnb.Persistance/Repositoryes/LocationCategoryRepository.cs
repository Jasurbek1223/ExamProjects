using AirBnb.Domain.Entities;
using AirBnb.Persistence.Caching;
using AirBnb.Persistence.DbContexts;
using AirBnb.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnb.Persistence.Repositories;

public class LocationCategoryRepository(
    LocationsDbContext dbContext,
    ICacheBroker cacheBroker)
    : EntityRepositoryBase<LocationCategory, LocationsDbContext>(dbContext, cacheBroker), ILocationCategoryRepository
{
    public new ValueTask<LocationCategory> CreateAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        locationCategory.Id = Guid.Empty;
        return base.CreateAsync(locationCategory, saveChanges, cancellationToken);
    }

    public new ValueTask<LocationCategory?> DeleteAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(locationCategory, saveChanges, cancellationToken);
    }

    public new ValueTask<LocationCategory?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }

    public new IQueryable<LocationCategory> Get(Expression<Func<LocationCategory, bool>>? predicate = null,
        bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<LocationCategory?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public new ValueTask<LocationCategory> UpdateAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(locationCategory, saveChanges, cancellationToken);
    }
}