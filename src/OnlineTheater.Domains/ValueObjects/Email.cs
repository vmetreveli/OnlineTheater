using System.Text.RegularExpressions;
using OnlineTheater.Domains.Primitives;


namespace OnlineTheater.Domains.ValueObjects;

public sealed class Email : ValueObject
{
    public string? Value { get; }

    private Email()
    {

    }
    private Email(string value)
        => Value = value;

    public static ErrorOr<Email> Create(string? email)
    {
        email = ( email ?? string.Empty ).Trim();
        if (email.Length == 0)
        {
            return Error.Validation(description: "email should not be empty");
        }

        if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
        {
            return Error.Validation(description: "Email is invalid");
        }

        return new Email(email);
    }


    public static explicit operator Email(string email)
    {
        return Create(email).Value;
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}