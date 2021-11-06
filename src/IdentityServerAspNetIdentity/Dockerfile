#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj", "src/IdentityServerAspNetIdentity/"]
RUN dotnet restore "src/IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj"
COPY . .
WORKDIR "/src/src/IdentityServerAspNetIdentity"
RUN dotnet build "IdentityServerAspNetIdentity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServerAspNetIdentity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServerAspNetIdentity.dll"]