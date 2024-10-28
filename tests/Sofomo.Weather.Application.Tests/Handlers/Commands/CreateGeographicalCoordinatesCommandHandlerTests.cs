using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Geometries;
using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Application.Commands;
using Sofomo.Weather.Application.Exceptions;
using Sofomo.Weather.Application.Handlers.Commands;
using Sofomo.Weather.Domain.Common;
using Sofomo.Weather.Domain.Entities;
using Xunit;

public class CreateGeographicalCoordinatesCommandHandlerTests
{
    private readonly Mock<ILocationRepository> _locationRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILogger<CreateGeographicalCoordinatesCommandHandler>> _loggerMock;
    private readonly CreateGeographicalCoordinatesCommandHandler _handler;

    public CreateGeographicalCoordinatesCommandHandlerTests()
    {
        _locationRepositoryMock = new Mock<ILocationRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _loggerMock = new Mock<ILogger<CreateGeographicalCoordinatesCommandHandler>>();
        _handler = new CreateGeographicalCoordinatesCommandHandler(
            _locationRepositoryMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task HandleAsync_ShouldCreateNewGeolocation_WhenLocationDoesNotExist()
    {
        // Arrange
        var command = new CreateGeographicalCoordinatesCommand(latitude: 40.7128, longitude: -74.0060);

        _locationRepositoryMock
            .Setup(repo => repo.GetLocationByPointAsync(It.IsAny<Point>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Geolocation)null); // Location does not exist

        _locationRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Geolocation>(), It.IsAny<CancellationToken>()))
            .Verifiable();

        _unitOfWorkMock
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        // Act
        await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        _locationRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Geolocation>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _loggerMock.VerifyLog(c => c.LogInformation("Creating geolocation {@Geolocation}", command));
    }

    [Fact]
    public async Task HandleAsync_ShouldThrowLocationExistException_WhenLocationAlreadyExists()
    {
        // Arrange
        var command = new CreateGeographicalCoordinatesCommand(latitude: 38.7128, longitude: -15.0060);

        var existingLocation = Geolocation.Create(Guid.NewGuid(), command.latitude, command.longitude);

        _locationRepositoryMock
            .Setup(repo => repo.GetLocationByPointAsync(It.IsAny<Point>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingLocation);

        // Act
        Func<Task> action = async () => await _handler.HandleAsync(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<LocationExistException>()
            .WithMessage($"Location with given cooridantes, latitude: {command.latitude} and longitude {command.longitude} already exists.");
    }
}