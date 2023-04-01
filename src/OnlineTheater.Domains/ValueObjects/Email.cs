using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace OnlineTheater.Domains.ValueObjects;

public sealed class Email : ValueObject<Email>
{
    public string Value { get; }

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

    protected override bool EqualsCore(Email other)
        => Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);

    protected override int GetHashCodeCore()
        => Value.GetHashCode();
}