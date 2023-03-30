using Referendum.Domain.Enums;
using Newtonsoft.Json;
using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.Entities;

public sealed class Movie: EntityBase
{
    public  string Name { get; set; }
    public  LicensingModel LicensingModel { get; set; }
}