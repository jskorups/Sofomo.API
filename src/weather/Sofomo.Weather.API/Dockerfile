#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sofomo.Weather.API/Sofomo.Weather.API.csproj", "Sofomo.Weather.API/"]
COPY ["Sofomo.Shared.Infrastructure/Sofomo.Shared.Infrastructure.csproj", "Sofomo.Shared.Infrastructure/"]
COPY ["Sofomo.Shared.Abstraction/Sofomo.Shared.Abstraction.csproj", "Sofomo.Shared.Abstraction/"]
COPY ["Sofomo.Wetaher.Application/Sofomo.Weather.Application.csproj", "Sofomo.Wetaher.Application/"]
COPY ["Sofomo.Weather.Domain/Sofomo.Weather.Domain.csproj", "Sofomo.Weather.Domain/"]
COPY ["Sofomo.Weather.Infrastructure/Sofomo.Weather.Infrastructure.csproj", "Sofomo.Weather.Infrastructure/"]
RUN dotnet restore "./Sofomo.Weather.API/./Sofomo.Weather.API.csproj"
COPY . .
WORKDIR "/src/Sofomo.Weather.API"
RUN dotnet build "./Sofomo.Weather.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sofomo.Weather.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sofomo.Weather.API.dll"]