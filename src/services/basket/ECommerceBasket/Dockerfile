#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/basket/ECommerceBasket/ECommerceBasket.csproj", "src/services/basket/ECommerceBasket/"]
COPY ["src/common/ECommerceCommon/ECommerceCommon.csproj", "src/common/ECommerceCommon/"]
RUN dotnet restore "src/services/basket/ECommerceBasket/ECommerceBasket.csproj"
COPY . .
WORKDIR "/src/src/services/basket/ECommerceBasket"
RUN dotnet build "ECommerceBasket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceBasket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceBasket.dll"]