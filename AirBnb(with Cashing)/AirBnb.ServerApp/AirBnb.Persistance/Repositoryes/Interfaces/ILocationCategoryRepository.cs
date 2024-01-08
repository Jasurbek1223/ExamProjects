using AirBnb.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnb.Persistence.Repositories.Interfaces;

public interface ILocationCategoryRepository
{
    IQueryable<LocationCategory> Get(Expression<Func<LocationCategory, bool>>? predicate = default,
        bool asNoTracking = false);

    ValueTask<LocationCategory?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory> CreateAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory> UpdateAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory?> DeleteAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}