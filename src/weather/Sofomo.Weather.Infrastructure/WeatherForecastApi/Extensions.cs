using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sofomo.Shared.Infrastructure;
using Sofomo.Weather.Application.Boundaries.Queries;
using Sofomo.Weather.Application.Boundaries.Repositories;
using Sofomo.Weather.Application.GeoCoordinates.Clients;
using Sofomo.Weather.Domain.Common;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Clients;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.DbInitializer;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Queries;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Repositories;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi;

public static class Extensions
{
    private const string _optionsSectionName = "WeatherForecastApi";

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var options = services.GetOptions<WeatherForecastApiOptions>(_optionsSectionName);
        services.AddSingleton<IWeatherForecastApiOptions, WeatherForecastApiOptions>(x => options);

        services.AddSingleton<IWeatherForecastHttpClient, WeatherForecastHttpClient>();

        var connectionString = configuration.GetConnectionString("Sofomo");

        services.AddDbContext<SofomoContext>(options =>
        {
            options.UseSqlServer(connectionString, x => x.UseNetTopologySuite());
        });

        services.AddScoped<IMigrationApplier, MigrationApplier>();
        services.AddScoped<IWeatherForecastSeeder, WeatherForecastSeeder>();
        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        services.AddScoped<ILocationQuery, LocationQuery>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}