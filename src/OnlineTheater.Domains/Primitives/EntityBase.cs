using System.ComponentModel.DataAnnotations.Schema;
using OnlineTheater.Domains.Primitives.Events;

namespace OnlineTheater.Domains.Primitives;

public abstract class EntityBase : IEquatable<EntityBase>
{
    // [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //Guid
    private readonly List<IDomainEvent> _domainEvents = new();

    protected EntityBase(Guid id) =>
        Id = id;

    protected EntityBase()
    {
    }

    public Guid Id { get; }

    public DateTime CreatedOn { get; }
    public DateTime? ModifiedOn { get; }

    public DateTime? DeletedOn { get; }
    public bool IsDelete { get; } = false;

    [NotMapped]
    public IEnumerable<IDomainEvent> DomainEvents =>
        _domainEvents.AsReadOnly();

    public bool Equals(EntityBase? other)
    {
        if (other is null) return false;

        return ReferenceEquals(this, other) || Id == other.Id;
    }


    public void AddDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();

    public static bool operator ==(EntityBase? a, EntityBase? b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Equals(b);
    }

    public static bool operator !=(EntityBase a, EntityBase b) => !( a == b );

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        if (obj is not EntityBase other) return false;

        return Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Id.GetHashCode();
}