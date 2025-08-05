using Chu.Bank.Inc.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddApiConfigurations();

    builder.Services.AddSwaggerConfigurations();

    builder.Services.AddIdentityConfigurations(builder.Configuration);

    builder.Services.AddJwtConfigurations(builder.Configuration);

    builder.Services.AddDependencyInjectionConfigurations();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerConfigurations();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}