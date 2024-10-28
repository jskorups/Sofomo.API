using Sofomo.Weather.Domain.Enums;

namespace Sofomo.Weather.Domain.Entities;

public class WeatherType
{
    public WeatherType()
    { }

    public WeatherType(WeatherCondition id, string description)
    {
        Id = id;
        Description = description;
    }

    public string Description { get; init; } = default!;

    public WeatherCondition Id { get; init; }

    public static WeatherType Create(WeatherCondition id, string description)
    {
        return new(
            id: id,
            description: description
        );
    }
}