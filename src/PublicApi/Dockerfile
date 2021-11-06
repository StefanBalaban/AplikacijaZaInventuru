#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5098
EXPOSE 5099
ENV ASPNETCORE_URLS=http://+:5098

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PublicApi/PublicApi.csproj", "src/PublicApi/"]
COPY ["src/ApplicationCore/ApplicationCore.csproj", "src/ApplicationCore/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/PublicApi/PublicApi.csproj"
COPY . .
WORKDIR "/src/src/PublicApi"
RUN dotnet build "PublicApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PublicApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublicApi.dll"]