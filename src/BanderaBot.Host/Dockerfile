FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BanderaBot.Host/BanderaBot.Host.csproj", "BanderaBot.Host/"]
RUN dotnet restore "BanderaBot.Host/BanderaBot.Host.csproj"
COPY . .
WORKDIR "/src/BanderaBot.Host"
RUN dotnet build "BanderaBot.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BanderaBot.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BanderaBot.Host.dll"]
