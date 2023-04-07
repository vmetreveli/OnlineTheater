using OnlineTheater.Domains.Entities;
using OnlineTheater.Domains.Primitives.Specifications;
using OnlineTheater.Domains.ValueObjects;

namespace OnlineTheater.Infrastructure.Specifications;

/// <summary>
///     Represents the specification for determining the user with email.
/// </summary>
public sealed class CustomerWithEmailSpecification : Specification<Customer>
{
    public CustomerWithEmailSpecification(Email email) : base(u => u.Email!.Value==email)
    {
    }
}