# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/VCardOnAbp.HttpApi.Host/VCardOnAbp.HttpApi.Host.csproj", "src/VCardOnAbp.HttpApi.Host/"]
COPY ["src/VCardOnAbp.Application/VCardOnAbp.Application.csproj", "src/VCardOnAbp.Application/"]
COPY ["src/VCardOnAbp.Domain/VCardOnAbp.Domain.csproj", "src/VCardOnAbp.Domain/"]
COPY ["src/VCardOnAbp.Domain.Shared/VCardOnAbp.Domain.Shared.csproj", "src/VCardOnAbp.Domain.Shared/"]
COPY ["src/VCardOnAbp.Application.Contracts/VCardOnAbp.Application.Contracts.csproj", "src/VCardOnAbp.Application.Contracts/"]
COPY ["src/VCardOnAbp.HttpApi/VCardOnAbp.HttpApi.csproj", "src/VCardOnAbp.HttpApi/"]
COPY ["src/VCardOnAbp.EntityFrameworkCore/VCardOnAbp.EntityFrameworkCore.csproj", "src/VCardOnAbp.EntityFrameworkCore/"]
COPY ["src/VCardOnAbp.HttpApi.Host/authserver.pfx", "src/VCardOnAbp.HttpApi.Host/"]
RUN dotnet restore "./src/VCardOnAbp.HttpApi.Host/VCardOnAbp.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/VCardOnAbp.HttpApi.Host"
RUN dotnet build "./VCardOnAbp.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VCardOnAbp.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VCardOnAbp.HttpApi.Host.dll"]