using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Repositories;

internal class WeatherForecastRepository(SofomoContext _context) : IWeatherForecastRepository
{
    public async Task AddRangeAsync(WeatherForecast[] weatherForecasts, CancellationToken cancellationToken)
        => await _context.WeatherForecasts.AddRangeAsync(weatherForecasts, cancellationToken);
}
