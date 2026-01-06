FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY FleetTracking.API/*.csproj ./FleetTracking.API/
COPY FleetTracking.Application/*.csproj ./FleetTracking.Application/
COPY FleetTracking.Core/*.csproj ./FleetTracking.Core/
COPY FleetTracking.Infrastructure/*.csproj ./FleetTracking.Infrastructure/

RUN dotnet restore

COPY . .
RUN dotnet publish FleetTracking.API/FleetTracking.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FleetTracking.API.dll"]