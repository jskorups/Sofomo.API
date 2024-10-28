using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Sofomo.Shared.Abstraction.Commands;
using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Application.Commands;
using Sofomo.Weather.Application.Exceptions;
using Sofomo.Weather.Domain.Common;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Domain.Utils;

namespace Sofomo.Weather.Application.Handlers.Commands;

public class DeleteGeographicalCoordinatesCommandHandler(
    ILocationRepository _locationRepository,
    ILogger<DeleteGeographicalCoordinatesCommandHandler> logger,
    IUnitOfWork _unitOfWork) : ICommandHandler<DeleteGeographicalCoordinatesCommand>
{
    public async Task HandleAsync(DeleteGeographicalCoordinatesCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting geolocation {@Geolocation}", command);

        Point pointToFind = GeolocationUtils.GetPoint(command.latitude, command.longitude);
        Geolocation? location = await _locationRepository.GetLocationByPointAsync(pointToFind, cancellationToken);

        if (location is null)
        {
            throw new LocationNotExistException(command.latitude, command.longitude);
        }

        _locationRepository.Remove(location);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}