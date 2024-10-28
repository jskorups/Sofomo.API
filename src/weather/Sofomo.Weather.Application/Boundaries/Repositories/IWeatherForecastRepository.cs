using Sofomo.Weather.Domain.Entities;

namespace Sofomo.Weather.Application.Boundaries.Repositories;

public interface IWeatherForecastRepository
{
    Task AddRangeAsync(WeatherForecast[] weatherForecasts, CancellationToken cancellationToken);
}