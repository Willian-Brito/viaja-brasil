FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["app/server/src/ViajaBrasil.API/ViajaBrasil.API.csproj", "src/ViajaBrasil.API/"]
COPY ["app/server/src/ViajaBrasil.Domain/ViajaBrasil.Domain.csproj", "src/ViajaBrasil.Domain/"]
COPY ["app/server/src/ViajaBrasil.Application/ViajaBrasil.Application.csproj", "src/ViajaBrasil.Application/"]
COPY ["app/server/src/ViajaBrasil.Infrastructure/ViajaBrasil.Infrastructure.csproj", "src/ViajaBrasil.Infrastructure/"]
RUN dotnet restore "src/ViajaBrasil.API/ViajaBrasil.API.csproj"
WORKDIR /src
COPY app/server/ .
RUN dotnet build "src/ViajaBrasil.API/ViajaBrasil.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/ViajaBrasil.API/ViajaBrasil.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_ENVIRONMENT=Docker
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/data && chmod -R 777 /app/data
ENTRYPOINT ["dotnet", "ViajaBrasil.API.dll"]
