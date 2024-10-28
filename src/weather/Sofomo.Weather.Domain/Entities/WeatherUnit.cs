namespace Sofomo.Weather.Domain.Entities;

public class WeatherUnit
{
    public Guid Id { get; init; }

    public virtual WeatherForecast? WeatherForecast { get; init; }
    public string? TimeUnit { get; init; }

    public string? WeatherCodeUnit { get; init; }

    public string? MaxTemperatureUnit { get; init; }

    public string? MaxUvIndexUnit { get; init; }

    public string? MinTemperatureUnit { get; init; }

    public string? RainSumUnit { get; init; }

    public static WeatherUnit Create(Guid id, string timeUnit, string weatherCodeUnit, string maxTemperatureUnit,
        string maxUvIndexUnit, string minTemperatureUnit, string rainSumUnit)
    {
        return new()
        {
            Id = id,
            TimeUnit = timeUnit,
            WeatherCodeUnit = weatherCodeUnit,
            MaxTemperatureUnit = maxTemperatureUnit,
            MinTemperatureUnit = minTemperatureUnit,
            MaxUvIndexUnit = maxUvIndexUnit,
            RainSumUnit = rainSumUnit
        };
    }
}