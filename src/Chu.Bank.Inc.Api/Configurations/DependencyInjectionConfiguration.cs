using Chu.Bank.Inc.Api.Configurations.Swagger;
using Chu.Bank.Inc.Api.Extensions;
using Chu.Bank.Inc.Api.Services;
using Chu.Bank.Inc.Domain.Entities.Users;
using FluentValidation;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chu.Bank.Inc.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddApiConfigurations();

        services.AddSwaggerConfigurations();

        services.AddIdentityConfigurations(configuration);

        services.AddJwtConfigurations(configuration);

        services.AddDependencyInjectionConfigurations();

        return services;
    }

    private static IServiceCollection AddDependencyInjectionConfigurations(this IServiceCollection services)
    {
        // AspNetUser
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IUser, AspNetUser>();

        // Identity
        services.AddSingleton<IJwtService, JwtService>();

        // Validators
        // services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        // Swagger
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }
}