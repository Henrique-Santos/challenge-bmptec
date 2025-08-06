using System.Reflection;
using Chu.Bank.Inc.Application.Behaviors;
using Chu.Bank.Inc.Domain.Services.Transactions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Chu.Bank.Inc.Application.Configurations;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // MediatR
        services.AddMediatR(options => 
        {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Validators
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        // Domain Services
        services.AddScoped<ITransactionPolicy, TransactionPolicy>();
        
        return services;
    }
}