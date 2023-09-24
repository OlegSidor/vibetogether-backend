#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["vibetogether-backend/vibetogether-backend.csproj", "vibetogether-backend/"]
RUN dotnet restore "vibetogether-backend/vibetogether-backend.csproj"
COPY . .
WORKDIR "/src/vibetogether-backend"
RUN dotnet build "vibetogether-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vibetogether-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vibetogether-backend.dll"]