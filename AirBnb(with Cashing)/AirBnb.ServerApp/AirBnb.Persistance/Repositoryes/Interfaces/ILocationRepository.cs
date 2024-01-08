using AirBnb.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnb.Persistence.Repositories.Interfaces;

public interface ILocationRepository
{
    IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<Location?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<Location> CreateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Location?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<Location?> DeleteAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}