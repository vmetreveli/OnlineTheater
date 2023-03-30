using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Referendum.Application.Abstractions.Authentication;
using Referendum.Application.Abstractions.Cryptography;
using Referendum.Domain.Repository;
using Referendum.Domain.Services;
using Referendum.Infrastructure.Authentication;
using Referendum.Infrastructure.Authentication.Settings;
using Referendum.Infrastructure.Context;
using Referendum.Infrastructure.Cryptography;
using Referendum.Infrastructure.Interceptors;
using Referendum.Infrastructure.Repositories;

namespace Referendum.Infrastructure;

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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]!))
            });

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SETTINGS_KEY));
        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();

        services.AddScoped<IPermissionService, PermissionService>();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}