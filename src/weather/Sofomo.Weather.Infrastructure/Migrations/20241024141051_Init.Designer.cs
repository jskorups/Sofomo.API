﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Sofomo.Weather.Infrastructure.WeatherForecastApi.Database.Context;

#nullable disable

namespace Sofomo.Weather.Infrastructure.Migrations
{
    [DbContext(typeof(SofomoContext))]
    [Migration("20241024141051_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sofomo.Weather.Domain.Entities.Geolocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.HasKey("Id");

                    b.ToTable("Geolocations", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
