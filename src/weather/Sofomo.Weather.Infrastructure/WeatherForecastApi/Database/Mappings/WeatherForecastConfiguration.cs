using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Mappings;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.ToTable(nameof(SofomoContext.WeatherForecasts));

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Geolocation)
            .WithMany(g => g.WeatherForecasts)
            .HasForeignKey(x => x.GeolocationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}