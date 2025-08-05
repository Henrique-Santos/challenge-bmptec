using Chu.Bank.Inc.Api.Configurations.Swagger;
using Chu.Bank.Inc.Api.Services;
using FluentValidation;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chu.Bank.Inc.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfigurations(this IServiceCollection services)
    {
        // Identity
        services.AddSingleton<IJwtService, JwtService>();

        // Validators
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        // Swagger
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }


}