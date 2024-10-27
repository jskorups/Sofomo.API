

using Sofomo.Weather.Domain.Enums;

namespace Sofomo.Weather.Domain.Entities;

public class WeatherForecast
{
    public WeatherForecast()
    {
    }

    public WeatherForecast(
        Geolocation geolocation,
        Guid id,
        WeatherCondition weatherTypeId,
        decimal maxTemperature,
        decimal maxUvIndex,
        decimal minTemperature,
        decimal rainSum,
        DateTime date,
        WeatherUnit weatherUnit)
    {
        Geolocation = geolocation;
        Id = id;
        WeatherTypeId = weatherTypeId;
        MaxTemperature = maxTemperature;
        MaxUvIndex = maxUvIndex;
        MinTemperature = minTemperature;
        RainSum = rainSum;
        Date = date;
        WeatherUnit = weatherUnit;
    }

    public WeatherForecast(
        Guid geolocationId,
        Guid id,
        WeatherCondition weatherTypeId,
        decimal maxTemperature,
        decimal maxUvIndex,
        decimal minTemperature,
        decimal rainSum,
        DateTime date,
        WeatherUnit weatherUnit)
    {
        GeolocationId = geolocationId;
        Id = id;
        WeatherTypeId = weatherTypeId;
        MaxTemperature = maxTemperature;
        MaxUvIndex = maxUvIndex;
        MinTemperature = minTemperature;
        RainSum = rainSum;
        Date = date;
        WeatherUnit = weatherUnit;
    }

    public DateTime Date { get; init; }

    public virtual Geolocation Geolocation { get; init; } = default!;

    public Guid GeolocationId { get; }

    public Guid Id { get; init; }

    public decimal MaxTemperature { get; init; }

    public decimal MaxUvIndex { get; init; }

    public decimal MinTemperature { get; init; }

    public decimal RainSum { get; init; }

    public virtual WeatherType WeatherType { get; init; } = default!;

    public WeatherCondition WeatherTypeId { get; init; }

    public virtual WeatherUnit WeatherUnit { get; init; } = default!;
     
    public Guid WeatherUnitId { get; init; }


    public static WeatherForecast Create(
        Guid id,
        Geolocation geolocation,
        int weatherTypeId,
        decimal maxTemperature,
        decimal minTemperature,
        decimal maxUvIndex,
        decimal rainSum,
        DateTime date,
        WeatherUnit weatherUnit
        )
    {
        //Ensure int to WeatherCondition
        return new(
            id: id,
            geolocation: geolocation,
            weatherTypeId: (WeatherCondition)weatherTypeId,
            maxTemperature: maxTemperature,
            minTemperature: minTemperature,
            maxUvIndex: maxUvIndex,
            rainSum: rainSum,
            date: date,
            weatherUnit: weatherUnit
        );
    }

    public static WeatherForecast Create(
       Guid id,
       Guid geolocationId,
       int weatherTypeId,
       decimal maxTemperature,
       decimal minTemperature,
       decimal maxUvIndex,
       decimal rainSum,
       DateTime date,
       WeatherUnit weatherUnit
       )
    {
        //Ensure int to WeatherCondition
        return new(
            id: id,
            geolocationId: geolocationId,
            weatherTypeId: (WeatherCondition)weatherTypeId,
            maxTemperature: maxTemperature,
            minTemperature: minTemperature,
            maxUvIndex: maxUvIndex,
            rainSum: rainSum,
            date: date,
            weatherUnit: weatherUnit
        );
    }
}
