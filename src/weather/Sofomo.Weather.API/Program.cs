using Serilog;
using Sofomo.Shared.Infrastructure.Commands;
using Sofomo.Shared.Infrastructure.Exceptions;
using Sofomo.Shared.Infrastructure.Queries;
using Sofomo.Shared.Infrastructure.Swagger;
using Sofomo.Weather.Infrastructure.WeatherForecastApi;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.DbSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) =>
               configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddQueries();
builder.Services.AddCommands();
builder.Services.AddErrorHandling();

builder.Services.AddSwagger();
var app = builder.Build();

app.UseErrorHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
await seeder.DbInitializeAsync();

// Configure the HTTP request pipeline.
app.UseSwaggerMiddleware();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();