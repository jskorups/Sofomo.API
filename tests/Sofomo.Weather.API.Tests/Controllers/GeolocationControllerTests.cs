using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sofomo.Shared.Abstraction.Commands;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.API.Controllers;
using Sofomo.Weather.Application.Commands;
using Sofomo.Weather.Application.Queries;
using Sofomo.Weather.Domain.DTOs;
using System.Net;
using Xunit;

namespace Sofomo.Weather.API.Tests.Controllers
{
    public class GeolocationControllerTests
    {
        private readonly Mock<IQueryDispatcher> _mockQueryDispatcher;
        private readonly Mock<ICommandDispatcher> _mockCommandDispatcher;
        private readonly GeolocationController _controller;

        public GeolocationControllerTests()
        {
            _mockQueryDispatcher = new Mock<IQueryDispatcher>();
            _mockCommandDispatcher = new Mock<ICommandDispatcher>();
            _controller = new GeolocationController(_mockQueryDispatcher.Object, _mockCommandDispatcher.Object);
        }

        [Fact]
        public async Task GetAllGeolocationCoordinatesAsync_ReturnsOkResult()
        {
            // Arrange
            _mockQueryDispatcher
                .Setup(q => q.QueryAsync(It.IsAny<GetAllGeographicalCoordinatesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CoordinatesDTO[0]);

            // Act
            var result = await _controller.GetAllGeolocationCoordinatesAsync(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            _mockQueryDispatcher.Verify(q => q.QueryAsync(It.IsAny<GetAllGeographicalCoordinatesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddGeolocationCoordinatesAsync_ReturnsCreatedStatusCode()
        {
            // Arrange
            var command = new CreateGeographicalCoordinatesCommand(latitude: 50, longitude: 40);

            _mockCommandDispatcher
                .Setup(c => c.SendAsync(command, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddGeolocationCoordinatesAsync(command, CancellationToken.None);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, statusCodeResult.StatusCode);
            _mockCommandDispatcher.Verify(c => c.SendAsync(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGeolocationCoordinatesAsync_ReturnsNoContentResult()
        {
            // Arrange
            var command = new DeleteGeographicalCoordinatesCommand(latitude: 50, longitude: 40);

            _mockCommandDispatcher
                .Setup(c => c.SendAsync(command, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteGeolocationCoordinatesAsync(command, CancellationToken.None);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);
            _mockCommandDispatcher.Verify(c => c.SendAsync(command, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}