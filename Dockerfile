FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Streaming/Streaming.csproj", "Streaming/Streaming.csproj"]
RUN dotnet restore "Streaming/Streaming.csproj"
COPY ["Streaming/.", "Streaming/"]
WORKDIR "/src/Streaming"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Streaming.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","Streaming.dll"]