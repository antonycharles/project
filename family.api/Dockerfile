FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Family.Api/*.csproj ./src/Family.Api/
COPY src/Family.Application/*.csproj ./src/Family.Application/
COPY src/Family.Domain/*.csproj ./src/Family.Domain/
COPY src/Family.Infrastructure/*.csproj ./src/Family.Infrastructure/

RUN dotnet restore

COPY src/Family.Api/. ./src/Family.Api/
COPY src/Family.Application/. ./src/Family.Application/
COPY src/Family.Domain/. ./src/Family.Domain/
COPY src/Family.Infrastructure/. ./src/Family.Infrastructure/

WORKDIR /app/src/Family.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Family.Api.dll"]