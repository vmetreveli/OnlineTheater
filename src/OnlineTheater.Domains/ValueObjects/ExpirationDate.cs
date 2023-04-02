using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class ExpirationDate : ValueObject
{
    public DateTime? Date { get; }
    public bool IsExpired => this != Infinite || Date < DateTime.UtcNow;

    public static readonly ExpirationDate Infinite = new(null);

    private ExpirationDate()
    {
    }

    private ExpirationDate(DateTime? date)
        => Date = date;

    public static ErrorOr<ExpirationDate> Create(DateTime date)
        => new ExpirationDate(date);


    public static implicit operator DateTime?(ExpirationDate date)
        => date.Date;


    public static explicit operator ExpirationDate(DateTime? date) =>
        date.HasValue ? Create(date.Value).Value : Infinite;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Date;
    }
}