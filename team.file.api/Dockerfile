FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Team.File.Api/*.csproj ./src/Team.File.Api/
COPY src/Team.File.Infrastructure/*.csproj ./src/Team.File.Infrastructure/

RUN dotnet restore

COPY src/Team.File.Api/. ./src/Team.File.Api/
COPY src/Team.File.Infrastructure/. ./src/Team.File.Infrastructure/

WORKDIR /app/src/Team.File.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Team.File.Api.dll"]