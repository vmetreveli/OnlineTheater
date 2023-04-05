using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public sealed class Movie : EntityBase
{
    public string? Name { get; private set; }
    public LicensingModel LicensingModel { get; private set; }

    public Dollars CalculatePrice(CustomerStatus status)
    {
        var modifier =1- status.GetDiscount();

        return LicensingModel switch
        {
            LicensingModel.TwoDays => Dollars.Of(4) * modifier,
            LicensingModel.LifeLong => Dollars.Of(8) * modifier,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ExpirationDate GetExpirationDate()
        => LicensingModel switch
        {
            LicensingModel.TwoDays => (ExpirationDate)DateTime.UtcNow.AddDays(2),
            LicensingModel.LifeLong => ExpirationDate.Infinite,
            _ => throw new ArgumentOutOfRangeException()
        };
}