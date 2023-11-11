#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Restaurant.Order.WebAPI/Restaurant.Order.WebAPI.csproj", "Restaurant.Order.WebAPI/"]
RUN dotnet restore "Restaurant.Order.WebAPI/Restaurant.Order.WebAPI.csproj"
COPY . .
WORKDIR "/src/Restaurant.Order.WebAPI"
RUN dotnet build "Restaurant.Order.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Restaurant.Order.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.Order.WebAPI.dll"]