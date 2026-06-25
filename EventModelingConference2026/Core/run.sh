#!/usr/bin/env bash
# Run the Core app for local development against a chosen database.
#
# Usage: ./run.sh [database] [--docker-only]
#   database        mongodb (default) | postgresql | mssql | sqlite
#   --docker-only   Start only the Docker Compose infrastructure and exit.
#
# The selected database determines the Chronicle projection sink type:
#   mongodb -> MongoDB sink, everything else -> SQL sink.

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"

DATABASE="mongodb"
DOCKER_ONLY=false
for arg in "$@"; do
    case "$arg" in
        mongodb|postgresql|mssql|sqlite) DATABASE="$arg" ;;
        --docker-only) DOCKER_ONLY=true ;;
        *) echo "Unknown argument: $arg (use mongodb|postgresql|mssql|sqlite [--docker-only])" >&2; exit 1 ;;
    esac
done

# Resolve the Chronicle projection sink type for the chosen database.
if [[ "$DATABASE" == "mongodb" ]]; then
    SINK_TYPE="MongoDB"
else
    SINK_TYPE="SQL"
fi

echo "▶ Starting infrastructure with docker compose (profile: $DATABASE)..."
docker compose --profile "$DATABASE" -f "$ROOT_DIR/docker-compose.yml" up -d
echo "✓ Chronicle  → http://localhost:8080  (Workbench)"

if [[ "$DOCKER_ONLY" == true ]]; then
    echo "✓ Infrastructure is up. Skipping app start (--docker-only)."
    exit 0
fi

echo "▶ Starting backend (dotnet watch) and frontend (yarn dev) — sink type: $SINK_TYPE..."

# Terminate both child processes when this script exits (e.g. on Ctrl+C).
trap 'kill 0' EXIT INT TERM

cd "$SCRIPT_DIR"
Cratis__Chronicle__DefaultSinkTypeId="$SINK_TYPE" dotnet watch run &
yarn dev &
wait
