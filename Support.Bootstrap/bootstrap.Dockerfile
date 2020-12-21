FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /bootstrap
COPY . .
RUN dotnet restore
WORKDIR /bootstrap/Support.Bootstrap
RUN dotnet build

FROM build AS publish
WORKDIR /bootstrap/Support.Bootstrap
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime
WORKDIR /bootstrap
COPY --from=publish /bootstrap/Support.Bootstrap/out ./
COPY --from=publish /bootstrap/Support.Bootstrap/bootstrap-entrypoint.sh ./
RUN chmod +x ./bootstrap-entrypoint.sh
RUN apk --update add postgresql-client
ENTRYPOINT ["sh", "bootstrap-entrypoint.sh"]