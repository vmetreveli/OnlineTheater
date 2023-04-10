using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class Dollars : ValueObject<Dollars>
{
    private const decimal MaxDollarAmount = 1_000_000;

    private Dollars()
    {
    }

    private Dollars(decimal? value)
        => Value = (decimal)value;

    public decimal Value { get; }
    public bool IsZero => Value==0;

    private static ErrorOr<Dollars> Create(decimal? dollarAmount)
    {
        switch (dollarAmount)
        {
            case null:
                return Error.Validation(description: "Dollar amount cannot be NULL");
            case < 0:
                return Error.Validation(description: "Dollar amount cannot be negative");
            case > MaxDollarAmount:
                return Error.Validation(description: $"Dollar amount cannot be great than {MaxDollarAmount}");
        }

        return dollarAmount % 0.01m > 0
            ? Error.Validation(description: "Dollar amount cannot contain part of  a penny")
            : new Dollars(dollarAmount);
    }

    public static Dollars Of(decimal value)
        => Create(value).Value;

    public static Dollars operator *(Dollars dollars, decimal multiplier) =>
        new(dollars.Value * multiplier);

    public static Dollars operator +(Dollars dollars1, Dollars dollars2) =>
        new(dollars1.Value + dollars2.Value);

    // public static explicit operator Dollars(decimal? value)
    //     => Create(value).Value;

    public static implicit operator decimal(Dollars dollars) =>
        dollars.Value;
}