using Chu.Bank.Inc.Api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Chu.Bank.Inc.Api.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfigurations(this IServiceCollection services)
    {
        // Identity
        services.AddSingleton<IJwtService, JwtService>();

        // Validators
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        return services;
    }


}