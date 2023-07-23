#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/BeymenGroupCase.ConfigurationApi/BeymenGroupCase.ConfigurationApi.csproj", "src/Services/BeymenGroupCase.ConfigurationApi/"]
RUN dotnet restore "src/Services/BeymenGroupCase.ConfigurationApi/BeymenGroupCase.ConfigurationApi.csproj"
COPY . .
WORKDIR "/src/src/Services/BeymenGroupCase.ConfigurationApi"
RUN dotnet build "BeymenGroupCase.ConfigurationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeymenGroupCase.ConfigurationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeymenGroupCase.ConfigurationApi.dll"]