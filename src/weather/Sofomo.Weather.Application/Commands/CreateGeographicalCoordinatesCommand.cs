using Sofomo.Shared.Abstraction.Commands;

namespace Sofomo.Weather.Application.Commands;

public record CreateGeographicalCoordinatesCommand(
       double latitude,
       double longitude) : ICommand;