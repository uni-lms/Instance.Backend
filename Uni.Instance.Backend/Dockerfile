FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Uni.Backend.csproj", "Uni.Backend/"]
RUN dotnet restore "Uni.Backend/Uni.Backend.csproj"
WORKDIR "/src/Uni.Backend"
COPY . .
RUN dotnet build "Uni.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Uni.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uni.Backend.dll"]
