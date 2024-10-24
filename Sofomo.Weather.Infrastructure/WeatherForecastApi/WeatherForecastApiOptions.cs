namespace Sofomo.Weather.Infrastructure.WeatherForecastApi;

public class WeatherForecastApiOptions : IWeatherForecastApiOptions
{
    public string? BaseUrl { get; set; }
}

public interface IWeatherForecastApiOptions
{
    public string? BaseUrl { get; set; }
}