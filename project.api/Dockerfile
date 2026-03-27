FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY project.api/*.sln ./project.api/
COPY project.api/src/Project.Api/*.csproj ./project.api/src/Project.Api/
COPY project.api/src/Project.Application/*.csproj ./project.api/src/Project.Application/
COPY project.api/src/Project.Domain/*.csproj ./project.api/src/Project.Domain/
COPY project.api/src/Project.Infrastructure/*.csproj ./project.api/src/Project.Infrastructure/
COPY common/Menssaging/Messaging.Abstractions/*.csproj ./common/Menssaging/Messaging.Abstractions/
COPY common/Menssaging/Messaging.Contracts/*.csproj ./common/Menssaging/Messaging.Contracts/
COPY common/Menssaging/Messaging.RabbitMQ/*.csproj ./common/Menssaging/Messaging.RabbitMQ/

RUN dotnet restore ./project.api/ProjectApi.sln

COPY project.api/src/Project.Api/. ./project.api/src/Project.Api/
COPY project.api/src/Project.Application/. ./project.api/src/Project.Application/
COPY project.api/src/Project.Domain/. ./project.api/src/Project.Domain/
COPY project.api/src/Project.Infrastructure/. ./project.api/src/Project.Infrastructure/
COPY common/Menssaging/Messaging.Abstractions/. ./common/Menssaging/Messaging.Abstractions/
COPY common/Menssaging/Messaging.Contracts/. ./common/Menssaging/Messaging.Contracts/
COPY common/Menssaging/Messaging.RabbitMQ/. ./common/Menssaging/Messaging.RabbitMQ/

WORKDIR /app/project.api/src/Project.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Project.Api.dll"]
