FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/File.Api/*.csproj ./src/File.Api/
COPY src/File.Infrastructure/*.csproj ./src/File.Infrastructure/

RUN dotnet restore

COPY src/File.Api/. ./src/File.Api/
COPY src/File.Infrastructure/. ./src/File.Infrastructure/

WORKDIR /app/src/File.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "File.Api.dll"]