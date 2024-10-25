using Sofomo.Shared.Abstraction.Exceptions;


namespace Sofomo.Weather.Application.Exceptions;

internal class LocationNotExistException : SofomoException
{
    public LocationNotExistException(double latitude, double longitude) : base($"Location with given cooridantes, latitude: '{latitude}', longitude: '{longitude}' not exist")
    {
    }
}
