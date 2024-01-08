using AirBnb.Api.Common.Filtering;
using AirBnb.Api.Common.Quering;
using AirBnb.Application.Common.Locations.Services;
using AirBnb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController(ILocationService locationService) : ControllerBase
{
    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination)
    {
        var result = locationService.Get().ApplyPagination(filterPagination);

        return new(result.Any() ? Ok(result) : NoContent());
    }

    [HttpGet("filter/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetbyCategoryId(
        [FromRoute] Guid categoryId,
        [FromQuery] FilterPagination filterPagination)
    {
        var result = await locationService.GetByCategoryIdAsync(categoryId, HttpContext.RequestAborted);

        return result.Any() ? Ok(result.ApplyPagination(filterPagination)) : NoContent();
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await locationService.GetByIdAsync(id, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] Location location, CancellationToken cancellationToken)
    {
        var result = await locationService.CreateAsync(location, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] Location location, CancellationToken cancellationToken)
    {
        var result = await locationService.UpdateAsync(location, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> Delete([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var result = await locationService.DeleteByIdAsync(id, cancellationToken: cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut("{id:guid}/uploadImage")]
    public async ValueTask<IActionResult> Upload(
        [FromRoute] Guid id,
        [FromServices] IWebHostEnvironment environment,
        IFormFile image,
        CancellationToken cancellationToken)
    {
        var result = await locationService.UploadImageAsync(id, image, environment.WebRootPath, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }
}