# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY "Tenark.sln" "Tenark.sln"
COPY ["src/Tenark.AuthServer/Tenark.AuthServer.csproj", "Tenark.AuthServer/Tenark.AuthServer.csproj"]
COPY ["src/Tenark.DbMigrator/Tenark.DbMigrator.csproj", "Tenark.DbMigrator/Tenark.DbMigrator.csproj"]
COPY ["src/Tenark.Application.Contracts/Tenark.Application.Contracts.csproj", "Tenark.Application.Contracts/Tenark.Application.Contracts.csproj"]
COPY ["src/Tenark.Domain.Shared/Tenark.Domain.Shared.csproj", "Tenark.Domain.Shared/Tenark.Domain.Shared.csproj"]
COPY ["src/Tenark.EntityFrameworkCore/Tenark.EntityFrameworkCore.csproj", "Tenark.EntityFrameworkCore/Tenark.EntityFrameworkCore.csproj"]
COPY ["src/Tenark.Domain/Tenark.Domain.csproj", "Tenark.Domain/Tenark.Domain.csproj"]

RUN dotnet restore "Tenark.sln"

COPY . .
WORKDIR "/src/Tenark.AuthServer"
RUN dotnet publish --no-restore -c Release -o /app

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tenark.AuthServer.dll"]