using NetTopologySuite.Geometries;
using Sofomo.Weather.Domain.Entities;

namespace Sofomo.Weather.Application.Boundaries.Repositories
{
    public interface ILocationRepository
    {
        Task AddAsync(Geolocation location, CancellationToken cancellationToken);

        Task<Geolocation?> GetLocationByPointAsync(Point location, CancellationToken cancellationToken);

        Task<Geolocation[]?> GetLocationsAsync(CancellationToken cancellationToken);

        void Remove(Geolocation location);
    }
}
