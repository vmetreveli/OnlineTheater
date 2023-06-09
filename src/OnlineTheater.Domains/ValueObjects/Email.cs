using System.Text.RegularExpressions;
using OnlineTheater.Domains.Primitives;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class Email : ValueObject
{
    private Email()
    {
    }

    private Email(string value)
        => Value = value;

    public string? Value { get; }

    public static ErrorOr<Email> Create(string? email)
    {
        email = ( email ?? string.Empty ).Trim();
        if (email.Length == 0) return Error.Validation(description: "email should not be empty");

        if (!Regex.IsMatch(email, @"^(.+)@(.+)$")) return Error.Validation(description: "Email is invalid");

        return new Email(email);
    }

    public static implicit operator string?(Email email)
        => email.Value;


    public static explicit operator Email(string? email)
        => Create(email).Value;


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value!;
    }
}