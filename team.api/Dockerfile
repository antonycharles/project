FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Team.Api/*.csproj ./src/Team.Api/
COPY src/Team.Application/*.csproj ./src/Team.Application/
COPY src/Team.Domain/*.csproj ./src/Team.Domain/
COPY src/Team.Infrastructure/*.csproj ./src/Team.Infrastructure/

RUN dotnet restore

COPY src/Team.Api/. ./src/Team.Api/
COPY src/Team.Application/. ./src/Team.Application/
COPY src/Team.Domain/. ./src/Team.Domain/
COPY src/Team.Infrastructure/. ./src/Team.Infrastructure/

WORKDIR /app/src/Team.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Team.Api.dll"]