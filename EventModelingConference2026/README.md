# Event Modeling Conference 2026 — Cratis Workshop

An event-sourced application built with **Cratis Arc** and **Cratis Chronicle**. Features are
organized as **vertical slices** — each slice keeps its command, events, read model, projection,
React UI, and specs together. The first feature is **Employment**, with an `Employees` area
containing two slices: `Hire` (state change) and `Listing` (state view).

The stack:

- **Backend** — .NET 10 / ASP.NET Core, Cratis Arc for CQRS, Cratis Chronicle for event sourcing.
- **Read models** — MongoDB, materialized by Chronicle projections (configured for camelCase).
- **Frontend** — React + TypeScript (Vite) with PrimeReact, served on its own dev server.
- **API reference** — [Scalar](https://scalar.com/) over the generated OpenAPI document.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) — runs Chronicle and the database
- [Node.js](https://nodejs.org/) + [Yarn](https://yarnpkg.com/)

## Layout

```text
EventModelingConference2026/
├── Core/                 ← the application (backend + .frontend + features)
│   ├── Employment/        ← the Employment feature (Employees: Hire, Listing)
│   ├── Program.cs         ← Arc + Chronicle + MongoDB setup, OpenAPI + Scalar
│   ├── run.sh             ← run everything directly (infra + backend + frontend)
│   └── .frontend/         ← Vite app (App.tsx, vite.config.ts, …)
├── Composition/           ← Aspire AppHost — orchestrates the distributed setup
│   └── run-*.sh           ← run via Aspire with a chosen database backend
└── docker-compose.yml     ← Chronicle + MongoDB + Aspire dashboard (used by Core/run.sh)
```

## Option A — Run directly from Core (recommended)

This starts the infrastructure with Docker Compose, then runs the backend and frontend as plain
processes. It is the quickest way to iterate on a slice.

```bash
cd Core
./run.sh                 # mongodb (default)
./run.sh postgresql      # or: mssql, sqlite
```

`run.sh` takes an optional database argument (`mongodb` | `postgresql` | `mssql` | `sqlite`,
defaulting to `mongodb`) and does three things:

1. Starts the Docker Compose infrastructure for that database (Chronicle + the database).
2. Sets the Chronicle projection sink type for that database — `MongoDB` for `mongodb`, `SQL` for
   the relational backends — via `Cratis__Chronicle__DefaultSinkTypeId`.
3. Runs the backend with `dotnet watch` on **http://localhost:5000** and the frontend (Vite) on
   **http://localhost:9000**.

To start only the infrastructure (for example, when you want to run the backend from your IDE):

```bash
./run.sh --docker-only            # or: ./run.sh postgresql --docker-only
```

Open the app at **http://localhost:9000** and navigate to **Employees**.

## Option B — Run with the Aspire Composition

The `Composition` project is an [.NET Aspire](https://learn.microsoft.com/dotnet/aspire/) AppHost
that provisions Chronicle and the chosen database, runs the Core backend (pinned to port 5000) and
its Vite frontend (port 9000), and shows everything in the Aspire dashboard. Choose a database
backend with the run script:

```bash
cd Composition
./run-mongodb.sh       # MongoDB (default)
./run-sqlite.sh        # SQLite
./run-mssql.sh         # Microsoft SQL Server
./run-postgresql.sh    # PostgreSQL
```

Each script sets `DATABASE_TYPE` and runs the AppHost. The AppHost selects the matching Chronicle
projection **sink** for that database and passes it to the services via
`Cratis__Chronicle__DefaultSinkTypeId` — `MongoDB` for the MongoDB backend, `SQL` for the
relational backends. The Aspire dashboard is available at **http://localhost:18888**.

## Viewing the API with Scalar

The backend exposes its OpenAPI document and a Scalar API reference UI:

- **Scalar UI** — http://localhost:5000/scalar/v1
- **OpenAPI document** — http://localhost:5000/openapi/v1.json

Scalar lists every generated endpoint, including the `Employment.Employees.Hire` command and the
`Employment.Employees.Listing` query, and lets you try requests directly from the browser.

## Useful endpoints

| Service | URL |
|---|---|
| Frontend | http://localhost:9000 |
| Backend API | http://localhost:5000 |
| Scalar API reference | http://localhost:5000/scalar/v1 |
| Chronicle Workbench | http://localhost:8080 |
| Aspire dashboard | http://localhost:18888 |
