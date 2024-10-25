using Sofomo.Shared.Abstraction.Exceptions;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Exceptions;

internal class ForecastNotFoundException : SofomoException
{
    public ForecastNotFoundException() : base("Forecast not found")
    {
    }
}
