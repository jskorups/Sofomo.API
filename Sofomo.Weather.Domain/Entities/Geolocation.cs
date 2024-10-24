using Sofomo.Weather.Domain.Utils;
using NetTopologySuite.Geometries;

namespace Sofomo.Weather.Domain.Entities
{
    public class Geolocation
    {
        public Guid Id { get; init; }

        public Point Location { get; init; } = default!;



        public static Geolocation Create(
            Guid id,
            double latitude,
            double longitude)
        {
  

            return new()
            {
                Id = id,
                Location = GeolocationUtils.GetPoint(latitude, longitude)
            };
        }
    }
}
