
using Microsoft.EntityFrameworkCore;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Mappings;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context
{
    internal class SofomoContext(DbContextOptions<SofomoContext> options) : DbContext(options)
    {
        public virtual DbSet<Geolocation> Geolocations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GeolocationConfiguration());

        }
    }
}
