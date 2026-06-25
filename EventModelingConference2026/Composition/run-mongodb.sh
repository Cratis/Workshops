#!/usr/bin/env bash
# Run the Library Composition (Aspire AppHost) with MongoDB (default).
# Usage: ./run-mongodb.sh

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "▶ Starting Composition AppHost with DATABASE_TYPE=mongodb..."
DATABASE_TYPE=mongodb dotnet run --project "$SCRIPT_DIR/Composition.csproj"
