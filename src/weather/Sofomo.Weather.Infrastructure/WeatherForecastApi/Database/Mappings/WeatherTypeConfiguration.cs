using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Domain.Enums;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Mappings;

public class WeatherTypeConfiguration : IEntityTypeConfiguration<WeatherType>
{
    public void Configure(EntityTypeBuilder<WeatherType> builder)
    {
        builder.ToTable(nameof(SofomoContext.WeatherTypes));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(255);

        foreach (WeatherCondition condition in Enum.GetValues(typeof(WeatherCondition)))
        {
            builder.HasData(
                WeatherType.Create(condition, condition.ToString())
            );
        }
    }
}