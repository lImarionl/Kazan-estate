# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5100
ENV ASPNETCORE_URLS=http://+:5100

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["KazanRealEstate.Api.csproj", "./"]
RUN dotnet restore "KazanRealEstate.Api.csproj"
COPY . .
RUN dotnet build "KazanRealEstate.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "KazanRealEstate.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KazanRealEstate.Api.dll"]
