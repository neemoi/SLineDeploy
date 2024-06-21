FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["StoreLineAPI/StoreLineAPI.csproj", "StoreLineAPI/"]
RUN dotnet restore "./StoreLineAPI/StoreLineAPI.csproj"

COPY . .
WORKDIR "/src/StoreLineAPI"
RUN dotnet build "./StoreLineAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./StoreLineAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "StoreLineAPI.dll"]
