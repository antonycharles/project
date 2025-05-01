FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Family.Accounts.Api/*.csproj ./src/Family.Accounts.Api/
COPY src/Family.Accounts.Application/*.csproj ./src/Family.Accounts.Application/
COPY src/Family.Accounts.Core/*.csproj ./src/Family.Accounts.Core/
COPY src/Family.Accounts.Infrastructure/*.csproj ./src/Family.Accounts.Infrastructure/
COPY tests/Family.Accounts.Api.Test/*.csproj ./tests/Family.Accounts.Api.Test/
COPY tests/Family.Accounts.Application.Test/*.csproj ./tests/Family.Accounts.Application.Test/

RUN dotnet restore

COPY src/Family.Accounts.Api/. ./src/Family.Accounts.Api/
COPY src/Family.Accounts.Application/. ./src/Family.Accounts.Application/
COPY src/Family.Accounts.Core/. ./src/Family.Accounts.Core/
COPY src/Family.Accounts.Infrastructure/. ./src/Family.Accounts.Infrastructure/
COPY tests/Family.Accounts.Api.Test/. ./tests/Family.Accounts.Api.Test/
COPY tests/Family.Accounts.Application.Test/. ./tests/Family.Accounts.Application.Test/

WORKDIR /app/src/Family.Accounts.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Family.Accounts.Api.dll"]