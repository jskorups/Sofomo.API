using FluentAssertions;
using Moq;
using Sofomo.Weather.Application.GeoCoordinates.Clients;
using Sofomo.Weather.Domain.DTOs;
using Sofomo.Weather.Infrastructure.Tests.WeatherForecastApi.Helpers;
using Sofomo.Weather.Infrastructure.WeatherForecastApi;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Clients;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Exceptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Sofomo.Weather.Tests.Unit.Clients;

public class WeatherForecastHttpClientTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<IWeatherForecastApiOptions> _optionsMock;
    private readonly MockHttpMessageHandler _mockHttpMessageHandler;

    private readonly IWeatherForecastHttpClient _weatherForecastHttpClient;

    public WeatherForecastHttpClientTests()
    {
        _optionsMock = new Mock<IWeatherForecastApiOptions>();
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _mockHttpMessageHandler = new MockHttpMessageHandler();

        var client = new HttpClient(_mockHttpMessageHandler) { BaseAddress = new Uri("https://localhost:5001") };
        _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

        _weatherForecastHttpClient = new WeatherForecastHttpClient(_httpClientFactoryMock.Object, _optionsMock.Object);
    }

    [Fact]
    public async Task GetWeatherForecastAsync_GivenValidLocation_ShouldReturnForecast()
    {
        // Arrange
        const double latitude = 40.7128;
        const double longitude = -74.0060;
        const string baseUrl = "https://localhost:5001";
        var expectedResponse = new WeatherForecastResponseDTO();
        var responseBody = JsonSerializer.Serialize(expectedResponse);

        _optionsMock.Setup(o => o.BaseUrl).Returns(baseUrl);
        _mockHttpMessageHandler.SetResponse(responseBody, HttpStatusCode.OK);

        // Act
        var result = await _weatherForecastHttpClient.GetWeatherForecastAsync(latitude, longitude, CancellationToken.None);

        // Assert
        _mockHttpMessageHandler.NumberOfCalls.Should().Be(1);
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetWeatherForecastAsync_GivenInvalidKey_ShouldThrowInvalidAuthorizationException()
    {
        // Arrange
        const double latitude = 40.7128;
        const double longitude = -74.0060;
        const string baseUrl = "https://localhost:5001";

        _optionsMock.Setup(o => o.BaseUrl).Returns(baseUrl);
        _mockHttpMessageHandler.SetResponse("Unauthorized", HttpStatusCode.Unauthorized);

        // Act & Assert
        await _weatherForecastHttpClient.Invoking(c => c.GetWeatherForecastAsync(latitude, longitude, CancellationToken.None))
            .Should().ThrowAsync<InvalidAuthorizationException>();
        _mockHttpMessageHandler.NumberOfCalls.Should().Be(1);
    }

    [Fact]
    public async Task GetWeatherForecastAsync_GivenNotFoundLocation_ShouldThrowForecastNotFoundException()
    {
        // Arrange
        const double latitude = 40.7128;
        const double longitude = -74.0060;
        const string baseUrl = "https://localhost:5001";

        _optionsMock.Setup(o => o.BaseUrl).Returns(baseUrl);
        _mockHttpMessageHandler.SetResponse("Not Found", HttpStatusCode.NotFound);

        // Act & Assert
        await _weatherForecastHttpClient.Invoking(c => c.GetWeatherForecastAsync(latitude, longitude, CancellationToken.None))
            .Should().ThrowAsync<ForecastNotFoundException>();
        _mockHttpMessageHandler.NumberOfCalls.Should().Be(1);
    }

    [Fact]
    public async Task GetWeatherForecastAsync_GivenServerError_ShouldThrowArgumentException()
    {
        // Arrange
        const double latitude = 40.7128;
        const double longitude = -74.0060;
        const string baseUrl = "https://localhost:5001";

        _optionsMock.Setup(o => o.BaseUrl).Returns(baseUrl);
        _mockHttpMessageHandler.SetResponse("Internal Server Error", HttpStatusCode.InternalServerError);

        // Act & Assert
        await _weatherForecastHttpClient.Invoking(c => c.GetWeatherForecastAsync(latitude, longitude, CancellationToken.None))
            .Should().ThrowAsync<ArgumentException>()
            .WithMessage("Error while fetching data from Maps API");
        _mockHttpMessageHandler.NumberOfCalls.Should().Be(1);
    }
}