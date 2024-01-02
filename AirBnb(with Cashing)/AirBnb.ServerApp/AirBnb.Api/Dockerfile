FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AirBnb.Api/AirBnb.Api.csproj", "AirBnb.Api/"]
RUN dotnet restore "AirBnb.Api/AirBnb.Api.csproj"
COPY . .
WORKDIR "/src/AirBnb.Api"
RUN dotnet build "AirBnb.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirBnb.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirBnb.Api.dll"]
