#!/usr/bin/env bash
# Run the Library Composition (Aspire AppHost) with SQLite.
# Usage: ./run-sqlite.sh

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "▶ Starting Composition AppHost with DATABASE_TYPE=sqlite..."
DATABASE_TYPE=sqlite dotnet run --project "$SCRIPT_DIR/Composition.csproj"
