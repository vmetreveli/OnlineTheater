using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineTheater.Applications.Behaviours;

namespace OnlineTheater.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}