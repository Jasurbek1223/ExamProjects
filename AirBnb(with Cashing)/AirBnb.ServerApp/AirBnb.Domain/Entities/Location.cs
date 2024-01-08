using AirBnb.Domain.Common.Models;
using System.Text.Json.Serialization;

namespace AirBnb.Domain.Entities;

public class Location : IEntity
{
    public Guid Id { get; set; }

    public string PlaceName { get; set; } = default!;

    public string BuiltIn { get; set; } = default!;

    public int AwayInKm { get; set; }

    public string Days { get; set; } = default!;

    public int Price { get; set; }

    public decimal Rate { get; set; }

    public string? ImageUrl { get; set; }

    public Guid CategoryId { get; set; }

    [JsonIgnore] public virtual LocationCategory? Category { get; set; } = default;
}