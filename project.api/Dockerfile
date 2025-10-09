FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Project.Api/*.csproj ./src/Project.Api/
COPY src/Project.Application/*.csproj ./src/Project.Application/
COPY src/Project.Domain/*.csproj ./src/Project.Domain/
COPY src/Project.Infrastructure/*.csproj ./src/Project.Infrastructure/

RUN dotnet restore

COPY src/Project.Api/. ./src/Project.Api/
COPY src/Project.Application/. ./src/Project.Application/
COPY src/Project.Domain/. ./src/Project.Domain/
COPY src/Project.Infrastructure/. ./src/Project.Infrastructure/

WORKDIR /app/src/Project.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Project.Api.dll"]