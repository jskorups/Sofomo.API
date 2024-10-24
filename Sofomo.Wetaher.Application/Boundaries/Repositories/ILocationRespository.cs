using Sofomo.Weather.Domain.Entities;

namespace Sofomo.Weather.Application.Boundaries.Repositories
{
    public interface ILocationRespository
    {
        Task AddAsync(Geolocation location, CancellationToken cancellationToken);
    }
}
