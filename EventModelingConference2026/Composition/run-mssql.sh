#!/usr/bin/env bash
# Run the Library Composition (Aspire AppHost) with Microsoft SQL Server.
# Usage: ./run-mssql.sh

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "▶ Starting Composition AppHost with DATABASE_TYPE=mssql..."
DATABASE_TYPE=mssql dotnet run --project "$SCRIPT_DIR/Composition.csproj"
