#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/BeymenGroupCase.ServiceB/BeymenGroupCase.ServiceB.csproj", "src/Services/BeymenGroupCase.ServiceB/"]
COPY ["src/Core/BeymenGroupCase.Configuration/BeymenGroupCase.Configuration.csproj", "src/Core/BeymenGroupCase.Configuration/"]
RUN dotnet restore "src/Services/BeymenGroupCase.ServiceB/BeymenGroupCase.ServiceB.csproj"
COPY . .
WORKDIR "/src/src/Services/BeymenGroupCase.ServiceB"
RUN dotnet build "BeymenGroupCase.ServiceB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeymenGroupCase.ServiceB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeymenGroupCase.ServiceB.dll"]