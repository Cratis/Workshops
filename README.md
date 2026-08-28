# Cratis Workshops

This repository contains **workshop material** for building event-sourced applications with
[Cratis](https://www.cratis.io) — using **Cratis Arc** (CQRS + full-stack type safety) and
**Cratis Chronicle** (event sourcing).

The material is hands-on: each workshop is a runnable application you can build, run, and explore
locally, with the backend, frontend, and event store wired together.

## Who these workshops are for

Developers who want hands-on experience with event sourcing and CQRS using the Cratis stack.
No prior Cratis experience is required — each workshop starts from a runnable application and
its README walks you through the setup. Familiarity with .NET and React helps, since the
workshops use ASP.NET Core on the backend and React + TypeScript on the frontend.

## Workshops

| Workshop | Description |
|---|---|
| [EventModelingConference2026](./EventModelingConference2026) | An event-sourced application built as vertical slices with Cratis Arc + Chronicle, a React + PrimeReact frontend, and MongoDB read models. Runnable against MongoDB (default), PostgreSQL, SQL Server, or SQLite through Chronicle's pluggable storage providers. See its [README](./EventModelingConference2026/README.md) for how to get started. |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (for the Chronicle event store and databases)
- [Node.js](https://nodejs.org/) with [Yarn](https://yarnpkg.com/) (the repo uses Yarn workspaces)

## Getting started

Pick a workshop from the table above and follow the README in its folder. Each workshop is
self-contained and explains how to run it both directly and through an Aspire-based composition.

## Learn more

- [Cratis documentation](https://www.cratis.io) — docs for the whole stack
- [Chronicle](https://github.com/Cratis/Chronicle) — event-sourcing database and processing runtime ([docs](https://www.cratis.io/chronicle/))
- [Arc](https://github.com/Cratis/Arc) — CQRS application framework for ASP.NET Core ([docs](https://www.cratis.io/arc/))
- [Samples](https://github.com/Cratis/Samples) — runnable event sourcing and CQRS samples for the Cratis stack

Cratis is open source and MIT licensed — including this workshop material.
