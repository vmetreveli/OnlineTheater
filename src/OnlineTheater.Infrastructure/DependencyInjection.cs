using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineTheater.Domains.Repository;
using OnlineTheater.Domains.Services;
using OnlineTheater.Infrastructure.Context;
using OnlineTheater.Infrastructure.Interceptors;
using OnlineTheater.Infrastructure.Repositories;
using OnlineTheater.Infrastructure.Service;

namespace OnlineTheater.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<UpdateEntitiesInterceptor>();
        services
            .AddDbContext<DbContext, DataContext>((sp, options) =>
                {
                    var auditableInterceptor = sp.GetService<UpdateEntitiesInterceptor>();
                    options.UseNpgsql(
                            configuration.GetConnectionString("DefaultConnection"))
                        .AddInterceptors(auditableInterceptor!);
                }
            );


        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IMovieService, MovieService>();
        return services;
    }
}