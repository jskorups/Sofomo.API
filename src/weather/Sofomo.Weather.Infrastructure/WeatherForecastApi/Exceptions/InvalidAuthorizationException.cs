using Sofomo.Shared.Abstraction.Exceptions;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Exceptions;

public class InvalidAuthorizationException : SofomoException
{
    public InvalidAuthorizationException() : base("Invalid authorization")
    {
    }
}