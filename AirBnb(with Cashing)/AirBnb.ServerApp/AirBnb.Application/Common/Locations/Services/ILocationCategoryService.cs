using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace AirBnb.Application.Common.Locations.Services;

public interface ILocationCategoryService
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

    ValueTask<string> UploadImageAsync(Guid id, IFormFile image, string webRootPath,
        CancellationToken cancellationToken = default);
}