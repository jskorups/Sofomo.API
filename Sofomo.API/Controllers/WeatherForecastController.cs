using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sofomo.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetWeatherForecastAsync([FromQuery] GetWeatherForecastRequest request, CancellationToken cancellationToken)
        {
            return View();
        }
    }
}
