namespace Sofomo.Weather.Domain.DTOs;

public class WeatherForecastDTO
{
    public DateTime Date { get; set; }

    public decimal MaxTemperature { get; set; }

    public decimal MaxUvIndex { get; set; }

    public decimal MinTemperature { get; set; }

    public decimal RainSum { get; set; }

    public string WeatherCode { get; set; } = default!;

    public WeatherUnitDTO WeatherUnits { get; set; } = default!;
}
