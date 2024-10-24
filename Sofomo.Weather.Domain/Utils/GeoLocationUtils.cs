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

        public static bool IsValidPoint(double latitude, double longitude)
        {
            return latitude >= -90 && latitude <= 90 && longitude >= -180 && longitude <= 180;
        }
    }
}
