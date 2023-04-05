using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Domains.Entities;

public  abstract class Movie : EntityBase
{
    public string? Name { get; private set; }
    private LicensingModel LicensingModel { get;  set; }

    public abstract ExpirationDate GetExpirationDate();

    public  Dollars CalculatePrice(CustomerStatus status)
    {
        var modifier = 1 - status.GetDiscount();
        return GetBasePrice() * modifier;
    }

    protected abstract Dollars GetBasePrice();

}

public sealed class TwoDaysMovie : Movie
{
    public override ExpirationDate GetExpirationDate() =>
        (ExpirationDate)DateTime.UtcNow.AddDays(2);

    protected override Dollars GetBasePrice() =>
        Dollars.Of(4);
}

public sealed class LifeLongMovie : Movie
{
    public override ExpirationDate GetExpirationDate() =>
        ExpirationDate.Infinite;

    protected override Dollars GetBasePrice() =>
        Dollars.Of(8);
}