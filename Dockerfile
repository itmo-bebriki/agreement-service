FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./*.props ./

COPY ["src/Itmo.Bebriki.Agreement/Itmo.Bebriki.Agreement.csproj", "src/Itmo.Bebriki.Agreement/"]

COPY ["src/Application/Itmo.Bebriki.Agreement.Application/Itmo.Bebriki.Agreement.Application.csproj", "src/Application/Itmo.Bebriki.Agreement.Application/"]
COPY ["src/Application/Itmo.Bebriki.Agreement.Application.Abstractions/Itmo.Bebriki.Agreement.Application.Abstractions.csproj", "src/Application/Itmo.Bebriki.Agreement.Application.Abstractions/"]
COPY ["src/Application/Itmo.Bebriki.Agreement.Application.Contracts/Itmo.Bebriki.Agreement.Application.Contracts.csproj", "src/Application/Itmo.Bebriki.Agreement.Application.Contracts/"]
COPY ["src/Application/Itmo.Bebriki.Agreement.Application.Models/Itmo.Bebriki.Agreement.Application.Models.csproj", "src/Application/Itmo.Bebriki.Agreement.Application.Models/"]

COPY ["src/Presentation/Itmo.Bebriki.Agreement.Presentation.Kafka/Itmo.Bebriki.Agreement.Presentation.Kafka.csproj", "src/Presentation/Itmo.Bebriki.Agreement.Presentation.Kafka/"]
COPY ["src/Presentation/Itmo.Bebriki.Agreement.Presentation.Grpc/Itmo.Bebriki.Agreement.Presentation.Grpc.csproj", "src/Presentation/Itmo.Bebriki.Agreement.Presentation.Grpc/"]

COPY ["src/Infrastructure/Itmo.Bebriki.Agreement.Infrastructure.Persistence/Itmo.Bebriki.Agreement.Infrastructure.Persistence.csproj", "src/Infrastructure/Itmo.Bebriki.Agreement.Infrastructure.Persistence/"]

RUN dotnet restore "src/Itmo.Bebriki.Agreement/Itmo.Bebriki.Agreement.csproj"

COPY . .
WORKDIR "/src/src/Itmo.Bebriki.Agreement"
RUN dotnet build "Itmo.Bebriki.Agreement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itmo.Bebriki.Agreement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Itmo.Bebriki.Agreement.dll"]