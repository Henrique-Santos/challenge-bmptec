using Chu.Bank.Inc.Api.Configurations.Swagger;
using Chu.Bank.Inc.Api.Services;
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
        // Identity
        services.AddSingleton<IJwtService, JwtService>();

        // Swagger
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        
        return services;
    }
}