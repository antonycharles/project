FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Family.Accounts.Login.Web/*.csproj ./src/Family.Accounts.Login.Web/
COPY src/Family.Accounts.Login.Infra/*.csproj ./src/Family.Accounts.Login.Infra/
COPY test/Family.Accounts.Login.Infra.Tests/*.csproj ./test/Family.Accounts.Login.Infra.Tests/

RUN dotnet restore

COPY src/Family.Accounts.Login.Web/. ./src/Family.Accounts.Login.Web/
COPY src/Family.Accounts.Login.Infra/. ./src/Family.Accounts.Login.Infra/
COPY test/Family.Accounts.Login.Infra.Tests/. ./test/Family.Accounts.Login.Infra.Tests/

WORKDIR /app/src/Family.Accounts.Login.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Family.Accounts.Login.Web.dll"]