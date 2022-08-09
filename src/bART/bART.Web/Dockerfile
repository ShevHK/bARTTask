#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["bART.Web/bART.Web.csproj", "bART.Web/"]
RUN dotnet restore "bART.Web/bART.Web.csproj"
COPY . .
WORKDIR "/src/bART.Web"
RUN dotnet build "bART.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bART.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bART.Web.dll"]