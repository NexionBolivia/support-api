FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app
COPY . .
RUN dotnet restore

# build api
WORKDIR /app/support-api
RUN dotnet build

# Hey dev! please uncomment this code so we have Unit tests, Integration Tests and API tests. thanks!

# # unit-test runner container
# FROM build as testrunner
# WORKDIR /app/Support.Services.Tests
# ENTRYPOINT ["dotnet", "test", "--logger:trx"]

# # integration-test runner container
# FROM build as integrationtestrunner
# WORKDIR /app/Support.Integration.Tests
# ENTRYPOINT ["dotnet", "test", "--logger:trx"]

# # api-test runner container
# FROM build as apitestrunner
# WORKDIR /app/Support.API.Tests
# ENTRYPOINT ["dotnet", "test", "--logger:trx"]

# # run unit-tests
# FROM build AS test
# WORKDIR /app/Support.Services.Tests
# RUN dotnet test --verbosity normal

# publish api
FROM build AS publish
RUN apk add dos2unix --update-cache --repository http://dl-3.alpinelinux.org/alpine/edge/community/ --allow-untrusted
WORKDIR /app
RUN dos2unix support-entrypoint.sh
WORKDIR /app/support-api
RUN dotnet publish -c Release -o out

ENTRYPOINT ["sh", "support-entrypoint.sh"]