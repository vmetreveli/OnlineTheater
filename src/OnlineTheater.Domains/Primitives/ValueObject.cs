using System.Reflection;

namespace OnlineTheater.Domains.Primitives;

public abstract class ValueObject<T> : IEquatable<T> where T : ValueObject<T>
{
    public override bool Equals(object? obj) =>
        Equals(obj as T);

    public bool Equals(T? other)
    {
        if (other == null)
        {
            return false;
        }

        // Compare all properties of the Value Object
        var properties = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var property in properties)
        {
            var thisValue = property.GetValue(this, null);
            var otherValue = property.GetValue(other, null);
            if (!Equals(thisValue, otherValue))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        // Combine hash codes of all properties of the Value Object
        var properties = GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public);

        return properties
            .Select(property => property
                .GetValue(this, null))
            .Aggregate(17, (current, value)
                => current * 31 + ( value != null ? value.GetHashCode() : 0 ));
    }

    public static bool operator ==(ValueObject<T>? a, ValueObject<T>? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject<T> a, ValueObject<T> b) => !( a == b );
}