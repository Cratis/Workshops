#!/usr/bin/env bash
# Run the Library Core app for local development.
# Usage: ./run.sh [--docker-only]
#   --docker-only   Start only the Docker Compose infrastructure (Chronicle + MongoDB) and exit.
#   (no option)     Start the infrastructure, then run the backend (dotnet watch) and
#                   frontend (yarn dev) as parallel processes.

set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"

echo "▶ Starting infrastructure with docker compose (MongoDB profile)..."
docker compose --profile mongodb -f "$ROOT_DIR/docker-compose.yml" up -d
echo "✓ Chronicle  → http://localhost:8080  (Workbench)"

if [[ "${1:-}" == "--docker-only" ]]; then
    echo "✓ Infrastructure is up. Skipping app start (--docker-only)."
    exit 0
fi

echo "▶ Starting backend (dotnet watch) and frontend (yarn dev)..."

# Terminate both child processes when this script exits (e.g. on Ctrl+C).
trap 'kill 0' EXIT INT TERM

cd "$SCRIPT_DIR"
dotnet watch run &
yarn dev &
wait
