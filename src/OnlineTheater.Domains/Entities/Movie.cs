using OnlineTheater.Domains.Primitives;
using Referendum.Domain.Enums;

namespace OnlineTheater.Domains.Entities;

public sealed class Movie : EntityBase
{
    public string Name { get; set; }
    public LicensingModel LicensingModel { get; set; }
}