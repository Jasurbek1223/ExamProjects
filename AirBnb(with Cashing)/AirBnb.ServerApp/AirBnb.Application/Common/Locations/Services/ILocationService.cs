using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace AirBnb.Application.Common.Locations.Services;

public interface ILocationService
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

    ValueTask<string> UploadImageAsync(Guid id, IFormFile image, string webRootPath,
        CancellationToken cancellationToken = default);

    ValueTask<IQueryable<Location>>
        GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
}