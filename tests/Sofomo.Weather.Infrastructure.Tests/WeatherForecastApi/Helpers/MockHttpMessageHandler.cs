using System.Net;

namespace Sofomo.Weather.Infrastructure.Tests.WeatherForecastApi.Helpers;

public class MockHttpMessageHandler : HttpMessageHandler
{
    private string _response = "{}";
    private HttpStatusCode _statusCode = HttpStatusCode.OK;

    public string? Input { get; private set; }
    public int NumberOfCalls { get; private set; }
    public string? RequestUrl { get; private set; }

    public void SetResponse(string response, HttpStatusCode statusCode)
    {
        _response = response;
        _statusCode = statusCode;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        NumberOfCalls++;
        if (request.Content is not null)
        {
            Input = await request.Content.ReadAsStringAsync();
        }

        if (request.RequestUri is not null)
        {
            RequestUrl = request.RequestUri.ToString();
        }

        return new HttpResponseMessage
        {
            StatusCode = _statusCode,
            Content = new StringContent(_response)
        };
    }
}
