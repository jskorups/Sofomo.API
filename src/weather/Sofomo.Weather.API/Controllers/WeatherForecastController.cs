using Microsoft.AspNetCore.Mvc;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.Application.Queries;

namespace Sofomo.Weather.API.Controllers
{
    [ApiController]
    [Route("api/weatherforecast")]
    public class WeatherForecastController(IQueryDispatcher queryDispatcher) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetWeatherForecastAsync([FromQuery] GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var result = await queryDispatcher.QueryAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}