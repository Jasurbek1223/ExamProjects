using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using AirBnb.Domain.Entities;

namespace AirBnb.Application.Common.Locations.Services;

public interface ILocationService
{
    IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<Location?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<Location> CreateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location?> DeleteByIdAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location?> DeleteAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location> UploadImageAsync(Guid id, IFormFile image, string webRootPath, CancellationToken cancellationToken = default);
}