using CSharpFunctionalExtensions;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class CustomerName : ValueObject<CustomerName>
{
    public string Value { get; }

    private CustomerName(string value)
        => Value = value;

    public static ErrorOr<CustomerName> Create(string customerName)
    {
        customerName = ( customerName ?? string.Empty ).Trim();
        if (customerName.Length == 0)
        {
            return Error.Validation(description:"Customer name should not be empty");
        }

        if (customerName.Length > 100)
        {
            return Error.Validation(description:"Customer name is too long");
        }

        return new CustomerName(customerName);
    }

    protected override bool EqualsCore(CustomerName other)
        => Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);

    protected override int GetHashCodeCore()
        => Value.GetHashCode();
}