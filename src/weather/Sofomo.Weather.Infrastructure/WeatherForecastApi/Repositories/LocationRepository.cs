using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Repositories;

internal class LocationRepository(SofomoContext _context) : ILocationRepository
{
    public async Task AddAsync(Geolocation location, CancellationToken cancellationToken)
         => await _context.AddAsync(location, cancellationToken);

    public async Task<Geolocation?> GetLocationByPointAsync(Point location, CancellationToken cancellationToken)
          => await _context.Geolocations.SingleOrDefaultAsync(x => x.Location.EqualsTopologically(location), cancellationToken);

    public async Task<Geolocation[]?> GetLocationsAsync(CancellationToken cancellationToken)
    => await _context.Geolocations.ToArrayAsync(cancellationToken);

    public void Remove(Geolocation location)
        => _context.Remove(location);
}