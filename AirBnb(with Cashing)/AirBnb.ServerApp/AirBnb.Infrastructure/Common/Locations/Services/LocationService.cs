using AirBnb.Application.Common.Locations.Services;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using AirBnb.Persistence.Repositories.Interfaces;

namespace AirBnb.Infrastructure.Common.Locations.Services;

public class LocationService(
    ILocationRepository locationRepository,
    IUrlService urlService) : ILocationService
{
    public ValueTask<Location> CreateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationRepository.CreateAsync(location, saveChanges, cancellationToken);
    }

    public ValueTask<Location?> DeleteAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationRepository.DeleteAsync(location, saveChanges, cancellationToken);
    }

    public ValueTask<Location?> DeleteByIdAsync(Guid id, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationRepository.DeleteByIdAsync(id, saveChanges, cancellationToken);
    }

    public IQueryable<Location> Get(Expression<Func<Location, bool>>? predicate = null, bool asNoTracking = false)
    {
        return locationRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<IQueryable<Location>> GetByCategoryIdAsync(Guid categoryId,
        CancellationToken cancellationToken = default)
    {
        return new(locationRepository.Get().Where(location => location.CategoryId == categoryId));
    }

    public ValueTask<Location?> GetByIdAsync(Guid id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return locationRepository.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return locationRepository.UpdateAsync(location, saveChanges, cancellationToken);
    }

    public async ValueTask<string> UploadImageAsync(Guid id, IFormFile image, string webRootPath,
        CancellationToken cancellationToken = default)
    {
        var found = await GetByIdAsync(id, cancellationToken: cancellationToken)
                    ?? throw new InvalidOperationException("Location not found with this Id");

        var relativePath = id.ToString() + image.FileName;
        var filePath = Path.Combine(webRootPath, relativePath);

        if (File.Exists(filePath)) File.Delete(filePath);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream, cancellationToken);
        }

        found.ImageUrl = await urlService.GetUrlFromRelativePath(relativePath);
        await UpdateAsync(found, cancellationToken: cancellationToken);

        return await urlService.GetUrlFromRelativePath(filePath);
    }
}