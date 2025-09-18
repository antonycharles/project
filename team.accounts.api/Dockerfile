FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Team.Accounts.Api/*.csproj ./src/Team.Accounts.Api/
COPY src/Team.Accounts.Application/*.csproj ./src/Team.Accounts.Application/
COPY src/Team.Accounts.Core/*.csproj ./src/Team.Accounts.Core/
COPY src/Team.Accounts.Infrastructure/*.csproj ./src/Team.Accounts.Infrastructure/
COPY tests/Team.Accounts.Api.Test/*.csproj ./tests/Team.Accounts.Api.Test/
COPY tests/Team.Accounts.Application.Test/*.csproj ./tests/Team.Accounts.Application.Test/

RUN dotnet restore

COPY src/Team.Accounts.Api/. ./src/Team.Accounts.Api/
COPY src/Team.Accounts.Application/. ./src/Team.Accounts.Application/
COPY src/Team.Accounts.Core/. ./src/Team.Accounts.Core/
COPY src/Team.Accounts.Infrastructure/. ./src/Team.Accounts.Infrastructure/
COPY tests/Team.Accounts.Api.Test/. ./tests/Team.Accounts.Api.Test/
COPY tests/Team.Accounts.Application.Test/. ./tests/Team.Accounts.Application.Test/

WORKDIR /app/src/Team.Accounts.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Team.Accounts.Api.dll"]