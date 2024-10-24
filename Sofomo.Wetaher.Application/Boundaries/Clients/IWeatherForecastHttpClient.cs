using Sofomo.Weather.Domain.DTOs;

namespace Sofomo.Weather.Application.GeoCoordinates.Clients;

public interface IWeatherForecastHttpClient
{
    Task<WeatherForecastResponseDTO?> GetWeatherForecastAsync(double latitude, double longitude, CancellationToken cancellationToken);
}