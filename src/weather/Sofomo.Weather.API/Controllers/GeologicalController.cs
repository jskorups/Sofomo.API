using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sofomo.Shared.Abstraction.Commands;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.Application.Commands;
using Sofomo.Weather.Application.Queries;
using Sofomo.Weather.Domain.DTOs;
using System.Net;

namespace Sofomo.Weather.API.Controllers;

[ApiController]
[Route("api/geolocation")]
public class GeolocationController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllGeolocationCoordinatesAsync(CancellationToken cancellationToken)
    {
        var result = await queryDispatcher.QueryAsync(new GetAllGeographicalCoordinatesQuery(), cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddGeolocationCoordinatesAsync([FromBody] CreateGeographicalCoordinatesCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteGeolocationCoordinatesAsync([FromBody] DeleteGeographicalCoordinatesCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command, cancellationToken);
        return NoContent();
    }


}
