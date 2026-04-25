FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

ARG GITHUB_PACKAGES_USERNAME
ARG GITHUB_PACKAGES_TOKEN

ENV GITHUB_PACKAGES_USERNAME=${GITHUB_PACKAGES_USERNAME}
ENV GITHUB_PACKAGES_TOKEN=${GITHUB_PACKAGES_TOKEN}

COPY project.api/*.sln ./project.api/
COPY project.api/NuGet.Config ./project.api/
COPY project.api/src/Project.Api/*.csproj ./project.api/src/Project.Api/
COPY project.api/src/Project.Application/*.csproj ./project.api/src/Project.Application/
COPY project.api/src/Project.Domain/*.csproj ./project.api/src/Project.Domain/
COPY project.api/src/Project.Infrastructure/*.csproj ./project.api/src/Project.Infrastructure/

RUN dotnet restore ./project.api/ProjectApi.sln --configfile ./project.api/NuGet.Config --no-cache --force --verbosity normal

COPY project.api/src/Project.Api/. ./project.api/src/Project.Api/
COPY project.api/src/Project.Application/. ./project.api/src/Project.Application/
COPY project.api/src/Project.Domain/. ./project.api/src/Project.Domain/
COPY project.api/src/Project.Infrastructure/. ./project.api/src/Project.Infrastructure/

WORKDIR /app/project.api/src/Project.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Project.Api.dll"]
