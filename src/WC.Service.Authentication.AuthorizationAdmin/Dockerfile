FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/WC.Service.Authentication.Domain/WC.Service.Authentication.Domain.csproj src/WC.Service.Authentication.Domain/
COPY src/WC.Service.Authentication.Domain.Abstractions/WC.Service.Authentication.Domain.Abstractions.csproj src/WC.Service.Authentication.Domain.Abstractions/
COPY src/WC.Service.Authentication.AuthorizationAdmin/WC.Service.Authentication.AuthorizationAdmin.csproj src/WC.Service.Authentication.AuthorizationAdmin/

RUN dotnet restore src/WC.Service.Authentication.AuthorizationAdmin/WC.Service.Authentication.AuthorizationAdmin.csproj

COPY src .
WORKDIR /src/WC.Service.Authentication.AuthorizationAdmin
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY src/WC.Service.Authentication.AuthorizationAdmin/appsettings.json /app/../..

ENTRYPOINT ["dotnet", "WC.Service.Authentication.AuthorizationAdmin.dll"]
