#!/bin/sh
# wait-for-postgres.sh .KB: https://docs.docker.com/compose/startup-order/

set -e

limit=1
max_attempts=35

until PGPASSWORD=${SUPPORT_DB_PASSWORD} psql -h "${SUPPORT_DB_SERVER}" -d "postgres" -U "${SUPPORT_DB_USER}" -p "${SUPPORT_DB_INTERNAL_PORT}" -c '\q' || [ $limit -gt $max_attempts ]; do
  >&2 echo "Postgres is unavailable - sleeping 5 secs (attempt $limit/$max_attempts)"
  limit=$((limit+1))
  sleep 5
done
  
>&2 echo "Postgres is UP"
>&2 echo "Now executing Bootstrap..."

dotnet Support.Bootstrap.dll