FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Family.Accounts.Management.Web/*.csproj ./src/Family.Accounts.Management.Web/
COPY src/Family.Accounts.Management.Infrastructure/*.csproj ./src/Family.Accounts.Management.Infrastructure/

RUN dotnet restore

COPY src/Family.Accounts.Management.Web/. ./src/Family.Accounts.Management.Web/
COPY src/Family.Accounts.Management.Infrastructure/. ./src/Family.Accounts.Management.Infrastructure/

WORKDIR /app/src/Family.Accounts.Management.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Family.Accounts.Management.Web.dll"]