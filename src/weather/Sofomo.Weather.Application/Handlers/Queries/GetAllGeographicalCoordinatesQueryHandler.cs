using Microsoft.Extensions.Logging;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Application.Queries;
using Sofomo.Weather.Domain.DTOs;
using Sofomo.Weather.Domain.Entities;

namespace Sofomo.Weather.Application.Handlers.Queries;

public class GetAllGeographicalCoordinatesQueryHandler(
    ILocationRepository _locationRepository,
    ILogger<GetAllGeographicalCoordinatesQueryHandler> logger) : IQueryHandler<GetAllGeographicalCoordinatesQuery, CoordinatesDTO[]?>
{
    public async Task<CoordinatesDTO[]?> HandleAsync(GetAllGeographicalCoordinatesQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all cooridinates");
        Geolocation[]? locations = await _locationRepository.GetLocationsAsync(cancellationToken);

        return locations?.Select(x => new CoordinatesDTO
        {
            Latitude = x.Location.Y,
            Longitude = x.Location.X,
        }).ToArray();
    }
}