using Sofomo.Shared.Infrastructure.Commands;
using Sofomo.Shared.Infrastructure.Queries;
using Sofomo.Shared.Infrastructure.Swagger;
using Sofomo.Weather.Infrastructure.WeatherForecastApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddWeatherForecastApi(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddQueries();
builder.Services.AddCommands();


builder.Services.AddSwagger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseSwaggerMiddleware();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
