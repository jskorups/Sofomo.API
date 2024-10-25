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

public class CreateGeographicalCoordinatesCommandHandler(
    ILocationRepository _locationRepository,
    ILogger<CreateGeographicalCoordinatesCommandHandler> logger,
    IUnitOfWork _unitOfWork) : ICommandHandler<CreateGeographicalCoordinatesCommand>
{
    public async Task HandleAsync(CreateGeographicalCoordinatesCommand command, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating geolocation {@Geolocation}", command);

        Point pointToFind = GeolocationUtils.GetPoint(command.latitude, command.longitude);
        Geolocation? location = await _locationRepository.GetLocationByPointAsync(pointToFind, cancellationToken);

        if (location is not null)
        {
            throw new LocationExistException(command.latitude, command.longitude);
        }

        Geolocation locationToStore = Geolocation.Create(
            Guid.NewGuid(),
            command.latitude,
            command.longitude
        );

        await _locationRepository.AddAsync(locationToStore, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }
}
