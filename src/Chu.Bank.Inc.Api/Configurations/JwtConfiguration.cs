using System.Text;
using Chu.Bank.Inc.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Chu.Bank.Inc.Api.Configurations;

public static class JwtConfiguration
{
    public static IServiceCollection AddJwtConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(JwtIdentity));

        services.Configure<JwtIdentity>(section);

        var settings = section.Get<JwtIdentity>();
        var key = Encoding.ASCII.GetBytes(settings!.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = settings!.Audience,
                ValidIssuer = settings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        return services;
    }
}