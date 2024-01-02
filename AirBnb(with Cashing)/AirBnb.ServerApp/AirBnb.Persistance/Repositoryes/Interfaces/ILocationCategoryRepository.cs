using System.Linq.Expressions;
using AirBnb.Domain.Entities;

namespace AirBnb.Persistance.Repositoryes.Interfaces;

public interface ILocationCategoryRepository
{
    IQueryable<LocationCategory> Get(Expression<Func<LocationCategory, bool>>? predicate = default,
        bool asNoTracking = false);

    ValueTask<LocationCategory> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory> CreateAsync(LocationCategory locationCategory, bool savechanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory> UpdateAsync(LocationCategory locationCategory, bool ssaveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    ValueTask<LocationCategory?> DeleteAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default);
}