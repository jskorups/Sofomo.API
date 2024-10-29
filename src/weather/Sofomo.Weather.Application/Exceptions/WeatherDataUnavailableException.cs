using Sofomo.Shared.Abstraction.Exceptions;

namespace Sofomo.Weather.Application.Exceptions
{
    internal class WeatherDataUnavailableException : SofomoException
    {
        public WeatherDataUnavailableException(double latitude, double longitude) : base($"Weather data for location with given cooridantes, latitude: '{latitude}', longitude: '{longitude}' could not be retrieved.")
        {
        }
    }
}
