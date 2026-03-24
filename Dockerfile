FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY NarSmart.sln .
COPY src/NarSmart.Domain/NarSmart.Domain.csproj src/NarSmart.Domain/
COPY src/NarSmart.Application/NarSmart.Application.csproj src/NarSmart.Application/
COPY src/NarSmart.Infrastructure/NarSmart.Infrastructure.csproj src/NarSmart.Infrastructure/
COPY src/NarSmart.API/NarSmart.API.csproj src/NarSmart.API/
COPY tests/NarSmart.Domain.Tests/NarSmart.Domain.Tests.csproj tests/NarSmart.Domain.Tests/
COPY tests/NarSmart.Application.Tests/NarSmart.Application.Tests.csproj tests/NarSmart.Application.Tests/

RUN dotnet restore

COPY . .

WORKDIR /src/src/NarSmart.API
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NarSmart.API.dll"]
