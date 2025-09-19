FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Accounts.Login.Web/*.csproj ./src/Accounts.Login.Web/
COPY src/Accounts.Login.Infra/*.csproj ./src/Accounts.Login.Infra/
COPY test/Accounts.Login.Infra.Tests/*.csproj ./test/Accounts.Login.Infra.Tests/

RUN dotnet restore

COPY src/Accounts.Login.Web/. ./src/Accounts.Login.Web/
COPY src/Accounts.Login.Infra/. ./src/Accounts.Login.Infra/
COPY test/Accounts.Login.Infra.Tests/. ./test/Accounts.Login.Infra.Tests/

WORKDIR /app/src/Accounts.Login.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Accounts.Login.Web.dll"]