using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.API.Controllers;
using Sofomo.Weather.Application.Queries;
using Sofomo.Weather.Domain.DTOs;
using Xunit;

namespace Sofomo.Weather.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IQueryDispatcher> _mockQueryDispatcher;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _mockQueryDispatcher = new Mock<IQueryDispatcher>();
            _controller = new WeatherForecastController(_mockQueryDispatcher.Object);
        }

        [Fact]
        public async Task GetWeatherForecastAsync_ReturnsOkResult()
        {

            var query = new GetWeatherForecastQuery(Latitude: 50, Longitude: 40);
            // Arrange
            _mockQueryDispatcher
                .Setup(q => q.QueryAsync(query, It.IsAny<CancellationToken>())).ReturnsAsync(new WeatherForecastDTO());

            // Act
            var result = await _controller.GetWeatherForecastAsync(query, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            _mockQueryDispatcher.Verify(q => q.QueryAsync(It.IsAny<GetWeatherForecastQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
