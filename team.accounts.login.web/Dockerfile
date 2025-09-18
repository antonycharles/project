FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Team.Accounts.Login.Web/*.csproj ./src/Team.Accounts.Login.Web/
COPY src/Team.Accounts.Login.Infra/*.csproj ./src/Team.Accounts.Login.Infra/
COPY test/Team.Accounts.Login.Infra.Tests/*.csproj ./test/Team.Accounts.Login.Infra.Tests/

RUN dotnet restore

COPY src/Team.Accounts.Login.Web/. ./src/Team.Accounts.Login.Web/
COPY src/Team.Accounts.Login.Infra/. ./src/Team.Accounts.Login.Infra/
COPY test/Team.Accounts.Login.Infra.Tests/. ./test/Team.Accounts.Login.Infra.Tests/

WORKDIR /app/src/Team.Accounts.Login.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Team.Accounts.Login.Web.dll"]