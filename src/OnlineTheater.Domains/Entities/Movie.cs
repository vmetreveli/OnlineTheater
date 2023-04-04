using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.Entities;

public sealed class Movie : EntityBase
{
    public string? Name { get; set; }
    public LicensingModel LicensingModel { get; set; }
}