using Sofomo.Shared.Abstraction.Exceptions;

namespace Sofomo.Weather.Application.Exceptions;

public  class LocationExistException : SofomoException
{
    public LocationExistException(double latitude, double longitude) : base($"Location with given cooridantes, latitude: '{latitude}', longitude: '{longitude}' already exist")
    {
    }
}
