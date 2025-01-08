FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj file into the container's /src/CardClashBackend directory
COPY ["CardClashBackend.csproj", "CardClashBackend/"]

WORKDIR /src/CardClashBackend

# Restore dependencies
RUN dotnet restore

# Copy everything else into the container
COPY . .

# Build the application
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardClashBackend.dll"]
