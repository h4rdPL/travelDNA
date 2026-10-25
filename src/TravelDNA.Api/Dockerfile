
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TravelDNA.sln ./

COPY src/TravelDNA.Domain/TravelDNA.Domain.csproj src/TravelDNA.Domain/
COPY src/TravelDNA.Application/TravelDNA.Application.csproj src/TravelDNA.Application/
COPY src/TravelDNA.Infrastructure/TravelDNA.Infrastructure.csproj src/TravelDNA.Infrastructure/
COPY src/TravelDNA.Api/TravelDNA.Api.csproj src/TravelDNA.Api/

RUN dotnet restore src/TravelDNA.Api/TravelDNA.Api.csproj

COPY src/ src/

RUN dotnet publish src/TravelDNA.Api/TravelDNA.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TravelDNA.Api.dll"]
