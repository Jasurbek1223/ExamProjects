using AirBnb.Application.Common.Locations.Services;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using AirBnb.Persistence.Repositories.Interfaces;

namespace AirBnb.Infrastructure.Common.Locations.Services;

public class LocationCategoryService(
    ILocationCategoryRepository locationCategoryRepository,
    IUrlService urlService) : ILocationCategoryService
{
    public ValueTask<LocationCategory> CreateAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationCategoryRepository.CreateAsync(locationCategory, saveChanges, cancellationToken);
    }

    public ValueTask<LocationCategory?> DeleteAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationCategoryRepository.DeleteAsync(locationCategory, saveChanges, cancellationToken);
    }

    public ValueTask<LocationCategory?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationCategoryRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }

    public IQueryable<LocationCategory> Get(Expression<Func<LocationCategory, bool>>? predicate = null,
        bool asNoTracking = false)
    {
        return locationCategoryRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<LocationCategory?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return locationCategoryRepository.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public ValueTask<LocationCategory> UpdateAsync(LocationCategory locationCategory, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationCategoryRepository.UpdateAsync(locationCategory, saveChanges, cancellationToken);
    }

    public async ValueTask<string> UploadImageAsync(Guid id, IFormFile image, string webRootPath,
        CancellationToken cancellationToken = default)
    {
        var found = await GetByIdAsync(id, cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException("LocationCategory not found with this Id");

        var relativePath = id.ToString() + image.FileName;

        var filePath = Path.Combine(webRootPath, relativePath);

        if (File.Exists(filePath)) File.Delete(filePath);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream, cancellationToken);
        }

        found.ImageUrl = await urlService.GetUrlFromRelativePath(relativePath);
        await UpdateAsync(found, cancellationToken: cancellationToken);

        return await urlService.GetUrlFromRelativePath(relativePath);
    }
}