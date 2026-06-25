#!/usr/bin/env bash
# Run the Library Composition (Aspire AppHost) with PostgreSQL.
# Usage: ./run-postgresql.sh

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "▶ Starting Composition AppHost with DATABASE_TYPE=postgresql..."
DATABASE_TYPE=postgresql dotnet run --project "$SCRIPT_DIR/Composition.csproj"
