using Sofomo.Shared.Abstraction.Commands;

namespace Sofomo.Weather.Application.Commands;

public record DeleteGeographicalCoordinatesCommand(
    double latitude,
    double longitude) : ICommand;