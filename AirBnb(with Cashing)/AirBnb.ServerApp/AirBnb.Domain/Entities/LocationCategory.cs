using AirBnb.Domain.Common.Models;
using System.Text.Json.Serialization;

namespace AirBnb.Domain.Entities;

public class LocationCategory : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string? ImageUrl { get; set; }

    [JsonIgnore] public virtual List<Location> Locations { get; set; } = new();
}