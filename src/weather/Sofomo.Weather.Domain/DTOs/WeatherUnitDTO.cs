namespace Sofomo.Weather.Domain.DTOs
{
    public class WeatherUnitDTO
    {
        public string Date { get; init; } = default!;
        public string MaxTemperature { get; init; } = default!;
        public string MaxUvIndex { get; init; } = default!;
        public string MinTemperature { get; init; } = default!;
        public string RainSum { get; init; } = default!;
        public string WeatherCode { get; set; } = default!;
    }
}