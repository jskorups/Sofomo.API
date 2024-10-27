using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sofomo.Weather.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingWeatherUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherCodeUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTemperatureUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxUvIndexUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinTemperatureUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RainSumUnit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxUvIndex = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RainSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeatherTypeId = table.Column<int>(type: "int", nullable: false),
                    WeatherUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_Geolocations_GeolocationId",
                        column: x => x.GeolocationId,
                        principalTable: "Geolocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_WeatherTypes_WeatherTypeId",
                        column: x => x.WeatherTypeId,
                        principalTable: "WeatherTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeatherForecasts_WeatherUnits_WeatherUnitId",
                        column: x => x.WeatherUnitId,
                        principalTable: "WeatherUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WeatherTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 0, "ClearSky" },
                    { 1, "MainlyClear" },
                    { 2, "PartlyCloudy" },
                    { 3, "Overcast" },
                    { 45, "Fog" },
                    { 48, "DepositingRimeFog" },
                    { 51, "DrizzleLight" },
                    { 53, "DrizzleModerate" },
                    { 55, "DrizzleDense" },
                    { 56, "FreezingDrizzleLight" },
                    { 57, "FreezingDrizzleDense" },
                    { 61, "RainSlight" },
                    { 63, "RainModerate" },
                    { 65, "RainHeavy" },
                    { 66, "FreezingRainLight" },
                    { 67, "FreezingRainHeavy" },
                    { 71, "SnowfallSlight" },
                    { 73, "SnowfallModerate" },
                    { 75, "SnowfallHeavy" },
                    { 77, "SnowGrains" },
                    { 80, "RainShowersSlight" },
                    { 81, "RainShowersModerate" },
                    { 82, "RainShowersViolent" },
                    { 85, "SnowShowersSlight" },
                    { 86, "SnowShowersHeavy" },
                    { 95, "ThunderstormSlight" },
                    { 96, "ThunderstormModerate" },
                    { 99, "ThunderstormHeavyHail" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_GeolocationId",
                table: "WeatherForecasts",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_WeatherTypeId",
                table: "WeatherForecasts",
                column: "WeatherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_WeatherUnitId",
                table: "WeatherForecasts",
                column: "WeatherUnitId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "WeatherTypes");

            migrationBuilder.DropTable(
                name: "WeatherUnits");
        }
    }
}
