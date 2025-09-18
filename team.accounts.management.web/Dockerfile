FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Team.Accounts.Management.Web/*.csproj ./src/Team.Accounts.Management.Web/
COPY src/Team.Accounts.Management.Infrastructure/*.csproj ./src/Team.Accounts.Management.Infrastructure/

RUN dotnet restore

COPY src/Team.Accounts.Management.Web/. ./src/Team.Accounts.Management.Web/
COPY src/Team.Accounts.Management.Infrastructure/. ./src/Team.Accounts.Management.Infrastructure/

WORKDIR /app/src/Team.Accounts.Management.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Team.Accounts.Management.Web.dll"]