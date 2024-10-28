using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Mappings
{
    public class GeolocationConfiguration : IEntityTypeConfiguration<Geolocation>
    {
        public void Configure(EntityTypeBuilder<Geolocation> builder)
        {
            builder.ToTable(nameof(SofomoContext.Geolocations));

            builder.HasKey(x => x.Id);

            builder.Property(b => b.Location)
               .HasColumnType("geography")
               .IsRequired();
        }
    }
}