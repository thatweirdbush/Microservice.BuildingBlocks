using Microservice.BuildingBlocks.Domain.ValueObjects;
using System.Collections.Concurrent;
using System.Reflection;

namespace Microservice.BuildingBlocks.Domain.Enumerations;

/// <summary>
/// Enumeration is pattern abstraction, which allows you to create "smart" enums with additional properties and methods.
/// <para><see href="https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types">Read more about the Enumeration pattern</see>.</para>
/// </summary>
/// <typeparam name="T">Enumeration class.</typeparam>
public abstract record Enumeration<T> : ValueObject
    where T : ValueObject
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo?> _valuePropertyCache = new();

    /// <summary>
    /// Uses reflection to get all the public static instances of itself.
    /// </summary>
    /// <returns>All the enumeration values.</returns>
    public static IEnumerable<T> GetAll()
    {
        return typeof(T)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(fieldInfo => fieldInfo.GetValue(null))
            .Cast<T>();
    }

    /// <summary>
    /// Gets an instance of the enumeration from its integer value.
    /// </summary>
    public static T? FromValue(int value)
    {
        var prop = _valuePropertyCache.GetOrAdd(typeof(T), type => type.GetProperty("Value"));
        
        if (prop == null || prop.PropertyType != typeof(int))
        {
            return null;
        }

        return GetAll().FirstOrDefault(item => (int)prop.GetValue(item)! == value);
    }
}