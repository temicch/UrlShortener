FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["UrlShortener.WebUI/UrlShortener.WebUI.csproj", "UrlShortener.WebUI/"]
COPY ["UrlShortener.Application.Interfaces/UrlShortener.Application.Interfaces.csproj", "UrlShortener.Application.Interfaces/"]
COPY ["UrlShortener.Domain/UrlShortener.Domain.csproj", "UrlShortener.Domain/"]
COPY ["UrlShortener.Infrastructure/UrlShortener.Infrastructure.csproj", "UrlShortener.Infrastructure/"]
COPY ["UrlShortener.Application.UseCases/UrlShortener.Application.UseCases.csproj", "UrlShortener.Application.UseCases/"]
COPY ["UrlShortener.Application.Implementation/UrlShortener.Application.Implementation.csproj", "UrlShortener.Application.Implementation/"]
RUN dotnet restore "UrlShortener.WebUI/UrlShortener.WebUI.csproj"
COPY . .
WORKDIR "/src/UrlShortener.WebUI"
RUN dotnet build "UrlShortener.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortener.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortener.WebUI.dll"]