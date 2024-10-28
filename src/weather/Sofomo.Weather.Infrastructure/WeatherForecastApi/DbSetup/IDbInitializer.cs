namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.DbSetup;

public interface IDbInitializer
{
    Task DbInitializeAsync();
}