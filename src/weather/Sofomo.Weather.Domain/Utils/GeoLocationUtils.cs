using NetTopologySuite.Geometries;

namespace Sofomo.Weather.Domain.Utils
{
    public static class GeolocationUtils
    {
        private static readonly int _spatialRefrenceIdentifier = 4326;

        public static Point GetPoint(double latitude, double longitude)
        {
            return new Point(longitude, latitude) { SRID = _spatialRefrenceIdentifier };
        }
    }
}