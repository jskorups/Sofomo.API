using Microsoft.EntityFrameworkCore;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.DbInitializer;

internal class MigrationApplier : IMigrationApplier
{
    private readonly SofomoContext _dbContext;

    public MigrationApplier(SofomoContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ApplyMigrationsAsync()
    {
        if (await _dbContext.Database.CanConnectAsync())
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
    }
}