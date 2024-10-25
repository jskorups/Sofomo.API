using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.Domain.DTOs;

namespace Sofomo.Weather.Application.Queries;

public record GetAllGeographicalCoordinatesQuery : IQuery<CoordinatesDTO[]?>;
