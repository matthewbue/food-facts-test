FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["FitnessFoodsApi/FitnessFoodsApi.csproj", "FitnessFoodsApi/"]
RUN dotnet restore "FitnessFoodsApi/FitnessFoodsApi.csproj"
COPY . .
WORKDIR "/src/FitnessFoodsApi"
RUN dotnet build "FitnessFoodsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FitnessFoodsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FitnessFoodsApi.dll"]
