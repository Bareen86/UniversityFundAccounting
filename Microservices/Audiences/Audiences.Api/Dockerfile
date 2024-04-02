#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/Audiences/Audiences.Api/Audiences.Api.csproj", "Microservices/Audiences/Audiences.Api/"]
COPY ["Microservices/Audiences/Audiences.Application/Audiences.Application.csproj", "Microservices/Audiences/Audiences.Application/"]
COPY ["Microservices/Audiences/Audiences.Domain/Audiences.Domain.csproj", "Microservices/Audiences/Audiences.Domain/"]
COPY ["Microservices/Audiences/Audiences.Infrastructure/Audiences.Infrastructure.csproj", "Microservices/Audiences/Audiences.Infrastructure/"]
COPY ["UniversityFundAccounting.Shared/UniversityFundAccounting.Shared.csproj", "RabbitMQShared/"]
RUN dotnet restore "Microservices/Audiences/Audiences.Api/Audiences.Api.csproj"
COPY . .
WORKDIR "/src/Microservices/Audiences/Audiences.Api"
RUN dotnet build "Audiences.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Audiences.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Audiences.Api.dll"]