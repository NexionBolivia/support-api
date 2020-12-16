FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /bootstrap
COPY . .
RUN dotnet restore
WORKDIR /bootstrap/support.bootstrap
RUN dotnet build

FROM build AS publish
WORKDIR /bootstrap/support.bootstrap
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /bootstrap
COPY --from=publish /bootstrap/support.bootstrap/out ./
ENTRYPOINT ["dotnet", "support.bootstrap.dll"]