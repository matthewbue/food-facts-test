FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5246

ENV ASPNETCORE_URLS=http://+:5246

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["FitnessFoods.API/FitnessFoods.API.csproj", "FitnessFoods.API/"]
RUN dotnet restore "FitnessFoods.API/FitnessFoods.API.csproj"
COPY . .
WORKDIR "/src/FitnessFoods.API"
RUN dotnet build "FitnessFoods.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "FitnessFoods.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitnessFoods.API.dll"]
