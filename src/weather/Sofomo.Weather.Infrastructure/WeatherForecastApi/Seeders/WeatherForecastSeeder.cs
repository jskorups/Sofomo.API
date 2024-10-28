using Microsoft.EntityFrameworkCore;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Domain.Enums;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Seeders;

internal class WeatherForecastSeeder(SofomoContext dbContext) : IWeatherForecastSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
        if (await dbContext.Database.CanConnectAsync())
            if (!dbContext.WeatherForecasts.Any())
            {
                var forecasts = GetForecasts();
                await dbContext.WeatherForecasts.AddRangeAsync(forecasts);
                await dbContext.SaveChangesAsync();
            }
    }

    private IEnumerable<WeatherForecast> GetForecasts()
    {
        var forecasts = new List<WeatherForecast>
{
    new()
    {
        Geolocation = new()
        {
            Id = Guid.NewGuid(),
            Location = new(4.625, -74.125)
        },
        Id = Guid.NewGuid(),
        WeatherTypeId = WeatherCondition.SnowGrains,
        MaxTemperature = 10,
        MaxUvIndex = 1,
        MinTemperature = 0,
        RainSum = 0,
        Date = DateTime.Now,
        WeatherUnit = new()
        {
            Id = Guid.NewGuid(),
            TimeUnit = "iso8601",
            WeatherCodeUnit = "wmo code",
            MaxTemperatureUnit = "C",
            MinTemperatureUnit = "C",
            MaxUvIndexUnit = "index",
            RainSumUnit = "mm"
        }
    },
    new()
    {
        Geolocation = new()
        {
            Id = Guid.NewGuid(),
            Location = new(34.0522, -118.2437) //  Los Angeles
        },
        Id = Guid.NewGuid(),
        WeatherTypeId = WeatherCondition.RainHeavy,
        MaxTemperature = 18,
        MaxUvIndex = 5,
        MinTemperature = 12,
        RainSum = 15,
        Date = DateTime.Now.AddDays(1),
        WeatherUnit = new()
        {
            Id = Guid.NewGuid(),
            TimeUnit = "iso8601",
            WeatherCodeUnit = "wmo code",
            MaxTemperatureUnit = "C",
            MinTemperatureUnit = "C",
            MaxUvIndexUnit = "index",
            RainSumUnit = "mm"
        }
    },
    new()
    {
        Geolocation = new()
        {
            Id = Guid.NewGuid(),
            Location = new(51.5074, -0.1278) //  London
        },
        Id = Guid.NewGuid(),
        WeatherTypeId = WeatherCondition.DepositingRimeFog,
        MaxTemperature = 22,
        MaxUvIndex = 7,
        MinTemperature = 15,
        RainSum = 0,
        Date = DateTime.Now.AddDays(2),
        WeatherUnit = new()
        {
            Id = Guid.NewGuid(),
            TimeUnit = "iso8601",
            WeatherCodeUnit = "wmo code",
            MaxTemperatureUnit = "C",
            MinTemperatureUnit = "C",
            MaxUvIndexUnit = "index",
            RainSumUnit = "mm"
        }
    }
//};
        return forecasts;
    }
}