# Cratis Workshops

This repository contains **workshop material** for building event-sourced applications with
[Cratis](https://www.cratis.io) — using **Cratis Arc** (CQRS + full-stack type safety) and
**Cratis Chronicle** (event sourcing).

The material is hands-on: each workshop is a runnable application you can build, run, and explore
locally, with the backend, frontend, and event store wired together.

## Workshops

| Workshop | Description |
|---|---|
| [EventModelingConference2026](./EventModelingConference2026) | An event-sourced application built as vertical slices with Cratis Arc + Chronicle, a React + PrimeReact frontend, and MongoDB read models. See its [README](./EventModelingConference2026/README.md) for how to get started. |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (for the Chronicle event store and databases)
- [Node.js](https://nodejs.org/) with [Yarn](https://yarnpkg.com/) (the repo uses Yarn workspaces)

## Getting started

Pick a workshop from the table above and follow the README in its folder. Each workshop is
self-contained and explains how to run it both directly and through an Aspire-based composition.
