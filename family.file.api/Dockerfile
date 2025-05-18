FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Family.File.Api/*.csproj ./src/Family.File.Api/
COPY src/Family.File.Infrastructure/*.csproj ./src/Family.File.Infrastructure/

RUN dotnet restore

COPY src/Family.File.Api/. ./src/Family.File.Api/
COPY src/Family.File.Infrastructure/. ./src/Family.File.Infrastructure/

WORKDIR /app/src/Family.File.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Family.File.Api.dll"]