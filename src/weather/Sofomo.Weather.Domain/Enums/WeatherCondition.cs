﻿namespace Sofomo.Weather.Domain.Enums;

public enum WeatherCondition
{
    ClearSky = 0,

    MainlyClear = 1,

    PartlyCloudy = 2,
    Overcast = 3,

    Fog = 45,

    DepositingRimeFog = 48,

    DrizzleLight = 51,

    DrizzleModerate = 53,
    DrizzleDense = 55,

    FreezingDrizzleLight = 56,

    FreezingDrizzleDense = 57,

    RainSlight = 61,

    RainModerate = 63,
    RainHeavy = 65,

    FreezingRainLight = 66,

    FreezingRainHeavy = 67,

    SnowfallSlight = 71,

    SnowfallModerate = 73,
    SnowfallHeavy = 75,

    SnowGrains = 77,

    RainShowersSlight = 80,

    RainShowersModerate = 81,
    RainShowersViolent = 82,

    SnowShowersSlight = 85,

    SnowShowersHeavy = 86,

    ThunderstormSlight = 95,

    ThunderstormModerate = 96,
    ThunderstormHeavyHail = 99
}