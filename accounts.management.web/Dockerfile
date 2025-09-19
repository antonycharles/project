FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
LABEL maintainer="Antony Charles"
WORKDIR /app

COPY *.sln .
COPY src/Accounts.Management.Web/*.csproj ./src/Accounts.Management.Web/
COPY src/Accounts.Management.Infrastructure/*.csproj ./src/Accounts.Management.Infrastructure/

RUN dotnet restore

COPY src/Accounts.Management.Web/. ./src/Accounts.Management.Web/
COPY src/Accounts.Management.Infrastructure/. ./src/Accounts.Management.Infrastructure/

WORKDIR /app/src/Accounts.Management.Web
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Accounts.Management.Web.dll"]