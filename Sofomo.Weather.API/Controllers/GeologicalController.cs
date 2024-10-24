using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sofomo.Shared.Abstraction.Commands;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.Application.Commands;
using System.Net;

namespace Sofomo.Weather.API.Controllers;

[ApiController]
[Route("api/geolocation")]
public class GeolocationController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddGeolocationCoordinatesAsync([FromBody] CreateGeographicalCoordinatesCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command, cancellationToken);

        return StatusCode((int)HttpStatusCode.Created);
    }
}
