using AirBnb.Api.Common.Filtering;
using AirBnb.Api.Common.Quering;
using AirBnb.Application.Common.Locations.Services;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationCategoriesController(ILocationCategoryService locationCategoryService) : ControllerBase
{
    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination)
    {
        var result = locationCategoryService.Get().ApplyPagination(filterPagination);

        return new(result.Any() ? Ok(result) : NoContent());
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await locationCategoryService.GetByIdAsync(id, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] LocationCategory locationCategory,
        CancellationToken cancellationToken)
    {
        var result = await locationCategoryService.CreateAsync(locationCategory, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] LocationCategory locationCategory,
        CancellationToken cancellationToken)
    {
        var result = await locationCategoryService.UpdateAsync(locationCategory, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var result = await locationCategoryService.DeleteByIdAsync(id, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut("{id:guid}/uploadImage")]
    public async ValueTask<IActionResult> Upload(
        [FromRoute] Guid id,
        [FromServices] IWebHostEnvironment environment,
        IFormFile image,
        CancellationToken cancellationToken)
    {
        var result =
            await locationCategoryService.UploadImageAsync(id, image, environment.WebRootPath, cancellationToken);
        return result is not null ? Ok(result) : BadRequest();
    }
}