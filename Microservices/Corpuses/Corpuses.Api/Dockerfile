#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/Corpuses/Corpuses.Api/Corpuses.Api.csproj", "Microservices/Corpuses/Corpuses.Api/"]
COPY ["Microservices/Corpuses/Corpuses.Domain/Corpuses.Domain.csproj", "Microservices/Corpuses/Corpuses.Domain/"]
COPY ["Microservices/Corpuses/Corpuses.Infrastructure/Corpuses.Infrastructure.csproj", "Microservices/Corpuses/Corpuses.Infrastructure/"]
RUN dotnet restore "Microservices/Corpuses/Corpuses.Api/Corpuses.Api.csproj"
COPY . .
WORKDIR "/src/Microservices/Corpuses/Corpuses.Api"
RUN dotnet build "Corpuses.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Corpuses.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corpuses.Api.dll"]