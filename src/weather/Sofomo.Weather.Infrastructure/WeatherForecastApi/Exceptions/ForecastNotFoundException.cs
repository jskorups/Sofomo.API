using Sofomo.Shared.Abstraction.Exceptions;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Exceptions;

public class ForecastNotFoundException : SofomoException
{
    public ForecastNotFoundException() : base("Forecast not found")
    {
    }
}
