using OnlineTheater.Domains.Primitives.Events;

namespace Referendum.Domain.Events;

/// <summary>
///     Represents the event that is raised when a user is created.
/// </summary>
public sealed class DistributorCreatedDomainEvent : IDomainEvent
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DistributorCreatedDomainEvent" /> class.
    /// </summary>
    /// <param name="distributor">The user.</param>
    // internal DistributorCreatedDomainEvent(Entities.Distributor distributor) => Distributor = distributor;

    /// <summary>
    ///     Gets the user.
    /// </summary>
    //   public Entities.Distributor Distributor { get; }
}