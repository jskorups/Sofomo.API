using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using Sofomo.Shared.Abstraction.Queries;
using Sofomo.Weather.Application.Boundaries.Queries;
using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Application.Exceptions;
using Sofomo.Weather.Application.GeoCoordinates.Clients;
using Sofomo.Weather.Application.Queries;
using Sofomo.Weather.Domain.Common;
using Sofomo.Weather.Domain.DTOs;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Domain.Utils;

namespace Sofomo.Weather.Application.Handlers.Queries
{
    public class GetWeatherForecastQueryHandler(
        IWeatherForecastHttpClient _weatherForecastHttpClient,
        IWeatherForecastRepository _weatherForecastRepository,
        IUnitOfWork _unitOfWork,
        ILocationQuery _locationQuery,
        ILogger<GetWeatherForecastQueryHandler> logger) : IQueryHandler<GetWeatherForecastQuery, WeatherForecastDTO>
    {
        public async Task<WeatherForecastDTO> HandleAsync(GetWeatherForecastQuery query, CancellationToken cancellationToken = default)
        {
            Point pointToFind = GeolocationUtils.GetPoint(query.Latitude, query.Longitude);

            GeolocationDTO? geolocationDbData = await _locationQuery.GetLocationWithLatestWeatherForecastByPointAsync(pointToFind, cancellationToken);

            if (geolocationDbData?.WeatherForecast is not null)
            {
                return geolocationDbData.WeatherForecast;
            }

            WeatherForecastResponseDTO externalResponse = await FetchWeatherData(query, cancellationToken);

            WeatherForecast[] weatherForecastsToStore = geolocationDbData?.Location is null
               ? CreateWeatherForecastsForNewLocation(query, externalResponse)
               : CreateWeatherForecastsForExistingLocation(geolocationDbData.GeolocationId, externalResponse);

            await SaveWeatherForecasts(weatherForecastsToStore, cancellationToken);

            WeatherForecast weatherForecastToReturn = weatherForecastsToStore
                .OrderByDescending(weatherForecast => weatherForecast.Date)
                .First();

            return CreateResponseFromWeatherForecast(weatherForecastToReturn);
        }

        private async Task<WeatherForecastResponseDTO> FetchWeatherData(GetWeatherForecastQuery query, CancellationToken cancellationToken)
        {
            var response = await _weatherForecastHttpClient.GetWeatherForecastAsync(query.Latitude, query.Longitude, cancellationToken);
            return response ?? throw new WeatherDataUnavailableException(query.Latitude, query.Longitude);
        }

        private static WeatherForecastDTO CreateResponseFromWeatherForecast(WeatherForecast weatherForecast)
        {
            return new WeatherForecastDTO
            {
                Date = weatherForecast.Date,
                MaxTemperature = weatherForecast.MaxTemperature,
                MaxUvIndex = weatherForecast.MaxUvIndex,
                MinTemperature = weatherForecast.MinTemperature,
                RainSum = weatherForecast.RainSum,
                WeatherCode = weatherForecast.WeatherTypeId.ToString(),
                WeatherUnits = new WeatherUnitDTO
                {
                    Date = weatherForecast.WeatherUnit.TimeUnit ?? "",
                    MaxTemperature = weatherForecast.WeatherUnit.MaxTemperatureUnit ?? "",
                    MinTemperature = weatherForecast.WeatherUnit.MinTemperatureUnit ?? "",
                    MaxUvIndex = weatherForecast.WeatherUnit.MaxUvIndexUnit ?? "",
                    RainSum = weatherForecast.WeatherUnit.RainSumUnit ?? "",
                    WeatherCode = weatherForecast.WeatherUnit.WeatherCodeUnit ?? "",
                }
            };
        }

        private static WeatherForecast[] CreateWeatherForecastsForNewLocation(GetWeatherForecastQuery query, WeatherForecastResponseDTO externalResponse)
        {
            List<WeatherForecast> weatherForecastsToStore = [];

            Geolocation location = Geolocation.Create(Guid.NewGuid(), query.Latitude, query.Longitude);

            for (int index = 0; index < externalResponse.Daily.Time.Length; index++)
            {
                WeatherForecast weatherForecastToStore = WeatherForecast.Create(
                    Guid.NewGuid(),
                    location,
                    externalResponse.Daily.WeatherCode[index],
                    externalResponse.Daily.Temperature2mMax[index],
                    externalResponse.Daily.Temperature2mMin[index],
                    externalResponse.Daily.UvIndexMax[index],
                    externalResponse.Daily.RainSum[index],
                    externalResponse.Daily.Time[index],
                    WeatherUnit.Create(
                        Guid.NewGuid(),
                        externalResponse.DailyUnits.Time,
                        externalResponse.DailyUnits.WeatherCode,
                        externalResponse.DailyUnits.Temperature2mMax,
                        externalResponse.DailyUnits.Temperature2mMin,
                        externalResponse.DailyUnits.UvIndexMax,
                        externalResponse.DailyUnits.RainSum
                        )

                );

                weatherForecastsToStore.Add(weatherForecastToStore);
            }

            return [.. weatherForecastsToStore];
        }

        private static WeatherForecast[] CreateWeatherForecastsForExistingLocation(Guid geolocationId, WeatherForecastResponseDTO externalResponse)
        {
            List<WeatherForecast> weatherForecastsToStore = [];

            for (int index = 0; index < externalResponse.Daily.Time.Length; index++)
            {
                WeatherForecast weatherForecastToStore = WeatherForecast.Create(
                    Guid.NewGuid(),
                    geolocationId,
                    externalResponse.Daily.WeatherCode[index],
                    externalResponse.Daily.Temperature2mMax[index],
                    externalResponse.Daily.Temperature2mMin[index],
                    externalResponse.Daily.UvIndexMax[index],
                    externalResponse.Daily.RainSum[index],
                    externalResponse.Daily.Time[index],
                     WeatherUnit.Create(
                        Guid.NewGuid(),
                        externalResponse.DailyUnits.Time,
                        externalResponse.DailyUnits.WeatherCode,
                        externalResponse.DailyUnits.Temperature2mMax,
                        externalResponse.DailyUnits.Temperature2mMin,
                        externalResponse.DailyUnits.UvIndexMax,
                        externalResponse.DailyUnits.RainSum
                        )
                );

                weatherForecastsToStore.Add(weatherForecastToStore);
            }

            return [.. weatherForecastsToStore.ToArray()];
        }

        private async Task SaveWeatherForecasts(WeatherForecast[] weatherForecastsToStore, CancellationToken cancellationToken)
        {
            await _weatherForecastRepository.AddRangeAsync(weatherForecastsToStore, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}