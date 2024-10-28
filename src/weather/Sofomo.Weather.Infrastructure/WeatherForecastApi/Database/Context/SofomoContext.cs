using Microsoft.EntityFrameworkCore;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Mappings;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context
{
    internal class SofomoContext(DbContextOptions<SofomoContext> options) : DbContext(options)
    {
        public virtual DbSet<Geolocation> Geolocations { get; set; }

        public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public virtual DbSet<WeatherType> WeatherTypes { get; set; }

        public virtual DbSet<WeatherUnit> WeatherUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new GeolocationConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherTypeConfiguration());
        }
    }
}