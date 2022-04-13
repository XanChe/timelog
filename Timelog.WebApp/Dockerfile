#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Timelog.WebApp/Timelog.WebApp.csproj", "Timelog.WebApp/"]
COPY ["Timelog.AspNetCore/Timelog.AspNetCore.csproj", "Timelog.AspNetCore/"]
COPY ["Timelog.Data/Timelog.Data.csproj", "Timelog.Data/"]
COPY ["Timelog.Core/Timelog.Core.csproj", "Timelog.Core/"]
COPY ["Timelog.Services/Timelog.Services.csproj", "Timelog.Services/"]
RUN dotnet restore "Timelog.WebApp/Timelog.WebApp.csproj"
COPY . .
WORKDIR "/src/Timelog.WebApp"
RUN dotnet build "Timelog.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Timelog.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Timelog.WebApp.dll"]