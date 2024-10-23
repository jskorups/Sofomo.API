using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text.Json;

namespace Sofomo.Shared.Infrastructure.Clients;

public class WeatherForecastHttpClient(IHttpClientFactory _httpClientFactory) : IWeatherForecastHttpClient
{
    public async Task<WeatherForecastExternalResponseDTO?> GetWeatherForecastAsync(double latitude, double longitude, CancellationToken cancellationToken)
    {
        HttpClient client = _httpClientFactory.CreateClient();

        var query = new Dictionary<string, string>()
        {
            [WeatherForecastHttpConfiguration.Latitude] = latitude.ToString().Replace(',', '.'),
            [WeatherForecastHttpConfiguration.Longitude] = longitude.ToString().Replace(',', '.'),
            [WeatherForecastHttpConfiguration.ForecastDays] = 1.ToString(),
            [WeatherForecastHttpConfiguration.Daily] = string.Join(",", WeatherForecastHttpConfiguration.Temperature2mMax, WeatherForecastHttpConfiguration.Temperature2mMin, WeatherForecastHttpConfiguration.WeatherCode, WeatherForecastHttpConfiguration.UvIndexMax, WeatherForecastHttpConfiguration.RainSum)
        };

        string uri = QueryHelpers.AddQueryString("https://api.open-meteo.com/v1/forecast", query!);

        HttpResponseMessage response = await client.GetAsync(uri, cancellationToken);

        return await TryParseResponse<WeatherForecastExternalResponseDTO>(response);
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
            HttpStatusCode.NotFound => throw new ArgumentException("NotFound"),
            HttpStatusCode.Unauthorized => throw new ArgumentException("Unauthorized"),
            _ => throw new ArgumentException("Error while fetching data from Maps API")
        };
    }
}
