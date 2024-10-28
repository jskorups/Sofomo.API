using Sofomo.Weather.Domain.Common;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Repositories;

internal class UnitOfWork(SofomoContext _context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);
}