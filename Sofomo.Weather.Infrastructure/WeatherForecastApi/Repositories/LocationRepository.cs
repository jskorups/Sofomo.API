using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Repositories;

internal class LocationRepository(SofomoContext _context) : ILocationRespository
{
    public async Task AddAsync(Geolocation location, CancellationToken cancellationToken)
         => await _context.AddAsync(location, cancellationToken);
}
