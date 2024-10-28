using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Sofomo.Weather.Application.Boundaries.Queries;
using Sofomo.Weather.Domain.DTOs;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Queries;

internal class LocationQuery(SofomoContext _context) : ILocationQuery
{
    public async Task<GeolocationDTO?> GetLocationWithLatestWeatherForecastByPointAsync(Point location, CancellationToken cancellationToken)
    {
        return await _context.Geolocations
            .Where(x => x.Location.EqualsTopologically(location))
            .Select(x => new GeolocationDTO
            {
                GeolocationId = x.Id,
                Location = x.Location,
                WeatherForecast = x.WeatherForecasts != null && x.WeatherForecasts.Count != 0
                    ? x.WeatherForecasts
                        .OrderByDescending(wf => wf.Date)
                        .Select(wf => new WeatherForecastDTO
                        {
                            MaxTemperature = wf.MaxTemperature,
                            MinTemperature = wf.MinTemperature,
                            MaxUvIndex = wf.MaxUvIndex,
                            RainSum = wf.RainSum,
                            Date = wf.Date,
                            WeatherCode = wf.WeatherType.Description,
                            WeatherUnits = new WeatherUnitDTO
                            {
                                Date = wf.WeatherUnit.TimeUnit,
                                WeatherCode = wf.WeatherUnit.WeatherCodeUnit,
                                MaxTemperature = wf.WeatherUnit.MaxTemperatureUnit,
                                MinTemperature = wf.WeatherUnit.MinTemperatureUnit,
                                RainSum = wf.WeatherUnit.RainSumUnit,
                                MaxUvIndex = wf.WeatherUnit.MaxUvIndexUnit
                            }
                        })
                        .FirstOrDefault()
                    : null
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}