using System.Text.Json.Serialization;

namespace Sofomo.Weather.Domain.DTOs;

public class DailyDTO
{
    [JsonPropertyName("rain_sum")]
    public decimal[] RainSum { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public decimal[] Temperature2mMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public decimal[] Temperature2mMin { get; set; }

    [JsonPropertyName("time")]
    public DateTime[] Time { get; set; }

    [JsonPropertyName("uv_index_max")]
    public decimal[] UvIndexMax { get; set; }

    [JsonPropertyName("weather_code")]
    public int[] WeatherCode { get; set; }
}

public class DailyUnitsDTO
{
    [JsonPropertyName("rain_sum")]
    public string RainSum { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public string Temperature2mMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public string Temperature2mMin { get; set; }

    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("uv_index_max")]
    public string UvIndexMax { get; set; }

    [JsonPropertyName("weather_code")]
    public string WeatherCode { get; set; }
}

public class WeatherForecastResponseDTO
{
    [JsonPropertyName("daily")]
    public DailyDTO Daily { get; set; }

    [JsonPropertyName("daily_units")]
    public DailyUnitsDTO DailyUnits { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("generation_time_ms")]
    public double GenerationTimeMs { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }
}
