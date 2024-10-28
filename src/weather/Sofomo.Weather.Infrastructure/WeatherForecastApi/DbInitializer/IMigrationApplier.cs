namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.DbInitializer;

public interface IMigrationApplier
{
    Task ApplyMigrationsAsync();
}