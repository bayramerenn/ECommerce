#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/order/ECommerceOrder/ECommerceOrder.csproj", "src/services/order/ECommerceOrder/"]
COPY ["src/common/ECommerceCommon/ECommerceCommon.csproj", "src/common/ECommerceCommon/"]
RUN dotnet restore "src/services/order/ECommerceOrder/ECommerceOrder.csproj"
COPY . .
WORKDIR "/src/src/services/order/ECommerceOrder"
RUN dotnet build "ECommerceOrder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerceOrder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceOrder.dll"]