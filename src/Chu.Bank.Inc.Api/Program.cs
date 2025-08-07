using Chu.Bank.Inc.Api.Configurations;
using Chu.Bank.Inc.Api.Middlewares;
using Chu.Bank.Inc.Application.Configurations;
using Chu.Bank.Inc.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerConfigurations();
    }

    app.UseAppConfigurations();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}