using Microsoft.AspNetCore.WebUtilities;
using Sofomo.Weather.Application.GeoCoordinates.Clients;
using Sofomo.Weather.Domain.DTOs;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Configuration;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Exceptions;
using System.Net;
using System.Text.Json;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Clients;

public class WeatherForecastHttpClient(IHttpClientFactory _httpClientFactory, IWeatherForecastApiOptions _options) : IWeatherForecastHttpClient
{
    public async Task<WeatherForecastResponseDTO?> GetWeatherForecastAsync(double latitude, double longitude, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        var query = new Dictionary<string, string>()
        {
            [WeatherForecastHttpConfiguration.Latitude] = latitude.ToString().Replace(',', '.'),
            [WeatherForecastHttpConfiguration.Longitude] = longitude.ToString().Replace(',', '.'),
            [WeatherForecastHttpConfiguration.ForecastDays] = 1.ToString(),
            [WeatherForecastHttpConfiguration.Daily] = string.Join(",", WeatherForecastHttpConfiguration.Temperature2mMax, WeatherForecastHttpConfiguration.Temperature2mMin, WeatherForecastHttpConfiguration.WeatherCode, WeatherForecastHttpConfiguration.UvIndexMax, WeatherForecastHttpConfiguration.RainSum)
        };

        string uri = QueryHelpers.AddQueryString(_options.BaseUrl, query!);

        HttpResponseMessage response = await client.GetAsync(uri, cancellationToken);

        return await TryParseResponse<WeatherForecastResponseDTO>(response);
    }

    private async Task<T?> TryParseResponse<T>(HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };

        return response.StatusCode switch
        {
            HttpStatusCode.OK => JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), options),
            HttpStatusCode.NotFound => throw new ForecastNotFoundException(),
            HttpStatusCode.Unauthorized => throw new InvalidAuthorizationException(),
            _ => throw new ArgumentException("Error while fetching data from Maps API")
        };
    }
}