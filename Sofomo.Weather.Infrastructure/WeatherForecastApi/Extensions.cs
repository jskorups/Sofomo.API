using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sofomo.Shared.Infrastructure;
using Sofomo.Weather.Application.GeoCoordinates.Clients;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Clients;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi;

public static class Extensions
{
    private const string _optionsSectionName = "WeatherForecastApi";

    public static void AddWeatherForecastApi(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.GetOptions<WeatherForecastApiOptions>(_optionsSectionName);
        services.AddSingleton<IWeatherForecastApiOptions, WeatherForecastApiOptions>(x => options);

        var connectionString = configuration.GetConnectionString("Sofomo");

        services.AddDbContext<SofomoContext>(options =>
        {
            options.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
        });

      


        services.AddHttpClient<IWeatherForecastHttpClient, WeatherForecastHttpClient>();
    }
}
