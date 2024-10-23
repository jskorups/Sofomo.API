namespace Sofomo.Shared.Infrastructure;

public class WeatherForecastApiOptions : IWeatherForecastApiOptions
{
    public string? BaseUrl { get; set; }
}

public interface IWeatherForecastApiOptions
{
    public string? BaseUrl { get; set; }
}