using CSharpFunctionalExtensions;
using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class CustomerName : ValueObject<CustomerName>
{
    public string Value { get; }

    private CustomerName(string value)
        => Value = value;

    public static ErrorOr<CustomerName> Create(string? customerName)
    {
        customerName = ( customerName ?? string.Empty ).Trim();
        return customerName.Length switch
        {
            0 => Error.Validation(description: "Customer name should not be empty"),
            > 100 => Error.Validation(description: "Customer name is too long"),
            _ => new CustomerName(customerName)
        };
    }


    protected override bool EqualsCore(CustomerName other)
    {
        return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    public static implicit operator string(CustomerName customerName)
    {
        return customerName.Value;
    }

    public static explicit operator CustomerName(string customerName)
    {
        return Create(customerName).Value;
    }
}