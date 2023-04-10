using OnlineTheater.Domains.Enums;
using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class CustomerStatus : ValueObject<CustomerStatus>
{
    public static readonly CustomerStatus Regular = new CustomerStatus(CustomerStatusType.Regular, ExpirationDate.Infinite);
    public bool IsAdvance => Type == CustomerStatusType.Advanced && !ExpirationDate.IsExpired;
    public CustomerStatusType? Type { get; }
    public ExpirationDate? ExpirationDate { get; }

    private CustomerStatus()
    {
    }

    private CustomerStatus(CustomerStatusType type, ExpirationDate expirationDate)
    {
        Type = type;
        ExpirationDate = expirationDate;
    }

    public decimal GetDiscount() => IsAdvance ? 0.25m : 0m;

    public CustomerStatus Promote() =>
        new CustomerStatus(CustomerStatusType.Advanced, (ExpirationDate)DateTime.UtcNow.AddYears(1));

}