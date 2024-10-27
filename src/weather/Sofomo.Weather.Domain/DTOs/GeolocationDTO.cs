using NetTopologySuite.Geometries;

namespace Sofomo.Weather.Domain.DTOs;
public class GeolocationDTO
{
    public Guid GeolocationId { get; init; }

    public Point? Location { get; init; }

    public WeatherForecastDTO? WeatherForecast { get; init; }
}
