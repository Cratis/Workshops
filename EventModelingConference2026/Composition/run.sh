#!/usr/bin/env bash
# Run the Composition (Aspire AppHost) against a chosen database.
#
# Usage: ./run.sh [database]
#   database   mongodb (default) | postgresql | mssql | sqlite
#
# The AppHost selects the matching Chronicle storage and projection sink type
# (MongoDB sink for mongodb, SQL sink for the relational databases) from DATABASE_TYPE.

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

DATABASE="${1:-mongodb}"
case "$DATABASE" in
    mongodb|postgresql|mssql|sqlite) ;;
    *) echo "Unknown database: $DATABASE (use mongodb|postgresql|mssql|sqlite)" >&2; exit 1 ;;
esac

echo "▶ Starting Composition AppHost with DATABASE_TYPE=$DATABASE..."
DATABASE_TYPE="$DATABASE" dotnet run --project "$SCRIPT_DIR/Composition.csproj"
