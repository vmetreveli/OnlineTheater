using System.Reflection;

namespace OnlineTheater.Applications.Contracts.Common.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile() =>
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

    // private void ApplyMappingsFromAssembly(Assembly assembly)
    // {
    //     var types = assembly.GetExportedTypes()
    //         .Where(t => t.GetInterfaces().Any(i =>
    //             i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
    //         .ToList();
    //
    //     foreach (var type in types)
    //     {
    //         var instance = Activator.CreateInstance(type);
    //
    //         var methodInfo = type.GetMethod("Mapping")
    //                          ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");
    //
    //         methodInfo?.Invoke(instance, new object[] {this});
    //     }
    // }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        }

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new[] {typeof(Profile)};

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
                methodInfo.Invoke(instance, new object[] {this});
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] {this});
                    }
            }
        }
    }
}