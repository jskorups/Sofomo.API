using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Domain.Enums;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.DbSetup;

internal class DbInitializer(SofomoContext dbContext) : IDbInitializer
{
    public async Task DbInitializeAsync()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
        if (await dbContext.Database.CanConnectAsync())
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }
            if (!dbContext.WeatherForecasts.Any())
            {
                var forecasts = GetForecasts();
                await dbContext.WeatherForecasts.AddRangeAsync(forecasts);
                await dbContext.SaveChangesAsync();
            }
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
            Location = new Point(-50, 14) { SRID = 4326 }
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
            Location = new Point(4.625, -74.125) { SRID = 4326 }
    },
        Id = Guid.NewGuid(),
        WeatherTypeId = WeatherCondition.SnowGrains,
        MaxTemperature = 35,
        MaxUvIndex = 1,
        MinTemperature = 0,
        RainSum = 0,
        Date = DateTime.Now,
        WeatherUnit = new()
        {
            Id = Guid.NewGuid(),
            TimeUnit = "iso8601",
            WeatherCodeUnit = "wmo code",
            MaxTemperatureUnit = "°C",
            MinTemperatureUnit = "°C",
            MaxUvIndexUnit = "index",
            RainSumUnit = "mm"
        }
    },
        new()
    {
        Geolocation = new()
        {
            Id = Guid.NewGuid(),
            Location = new Point(50.7816, 17.0648) { SRID = 4326 }
    },
        Id = Guid.NewGuid(),
        WeatherTypeId = WeatherCondition.Fog,
        MaxTemperature = 34,
        MaxUvIndex = 6,
        MinTemperature = -12,
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
};
        return forecasts;
    }
}