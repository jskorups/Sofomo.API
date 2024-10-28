﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sofomo.Weather.Domain.Entities;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

namespace Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Mappings
{
    public class WeatherUnitConfiguration
    {
        public void Configure(EntityTypeBuilder<WeatherUnit> builder)
        {
            builder.ToTable(nameof(SofomoContext.WeatherUnits));

            builder.HasKey(x => x.Id);
        }
    }
}