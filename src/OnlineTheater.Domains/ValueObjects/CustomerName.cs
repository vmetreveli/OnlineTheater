using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class CustomerName : ValueObject
{
    private CustomerName(string value)
        => Value = value;

    public string Value { get; }

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

    public static implicit operator string?(CustomerName name)
        => name.Value;


    public static explicit operator CustomerName(string? name)
        => Create(name).Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}