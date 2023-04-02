using System.Reflection;

namespace OnlineTheater.Domains.Primitives;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> _enumerations = CreateEnumerations();

    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    protected Enumeration()
    {
        Value = default;
        Name = string.Empty;
    }

    public int Value { get; protected init; }

    public string Name { get; protected init; }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null) return false;

        return GetType() == other.GetType() && other.Value.Equals(Value);
    }

    public int CompareTo(Enumeration<TEnum>? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static TEnum? FromValue(int value) =>
        _enumerations.TryGetValue(value, out var enumeration)
            ? enumeration
            : default;

    public static TEnum? FromName(string name) =>
        _enumerations.Values.SingleOrDefault(e => e.Name == name);


    public static bool operator ==(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        if (a is null && b is null) return true;

        if (a is null || b is null) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum>? a, Enumeration<TEnum>? b) => !( a == b );

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (!( obj is Enumeration<TEnum> otherValue )) return false;

        return GetType() == obj.GetType() && otherValue.Value.Equals(Value);
    }

    public override int GetHashCode() => Value.GetHashCode();


    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);
        var fieldForType = enumerationType.GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
        return fieldForType.ToDictionary(x => x.Value);
    }

    public static IReadOnlyCollection<TEnum> GetValues() =>
        _enumerations.Values.ToList();
}