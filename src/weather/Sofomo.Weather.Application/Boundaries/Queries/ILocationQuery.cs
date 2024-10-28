using NetTopologySuite.Geometries;
using Sofomo.Weather.Domain.DTOs;

namespace Sofomo.Weather.Application.Boundaries.Queries;

public interface ILocationQuery
{
    Task<GeolocationDTO?> GetLocationWithLatestWeatherForecastByPointAsync(Point location, CancellationToken cancellationToken);
}