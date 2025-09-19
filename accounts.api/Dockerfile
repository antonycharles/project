FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Accounts.Api/*.csproj ./src/Accounts.Api/
COPY src/Accounts.Application/*.csproj ./src/Accounts.Application/
COPY src/Accounts.Core/*.csproj ./src/Accounts.Core/
COPY src/Accounts.Infrastructure/*.csproj ./src/Accounts.Infrastructure/
COPY tests/Accounts.Api.Test/*.csproj ./tests/Accounts.Api.Test/
COPY tests/Accounts.Application.Test/*.csproj ./tests/Accounts.Application.Test/

RUN dotnet restore

COPY src/Accounts.Api/. ./src/Accounts.Api/
COPY src/Accounts.Application/. ./src/Accounts.Application/
COPY src/Accounts.Core/. ./src/Accounts.Core/
COPY src/Accounts.Infrastructure/. ./src/Accounts.Infrastructure/
COPY tests/Accounts.Api.Test/. ./tests/Accounts.Api.Test/
COPY tests/Accounts.Application.Test/. ./tests/Accounts.Application.Test/

WORKDIR /app/src/Accounts.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Accounts.Api.dll"]