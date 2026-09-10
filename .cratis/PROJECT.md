# Workshops — project context

Hands-on workshop material for building event-sourced applications with
Cratis — using Cratis Arc (CQRS + full-stack type safety) and Cratis Chronicle
(event sourcing). Each workshop is a runnable application you can build, run,
and explore; the material steps participants through it.

## Conventions

- Workshop steps compile against released packages so the material works
  outside the Cratis repositories.

## AI-assisted development

This repository uses the Cratis AI contract:

- **`.cratis/ai.json`** records the subscription — `cratis/documentation` plus the language-agnostic `cratis/engineering/core` cell.
- **`.cratis/PROJECT.md`** (this file) is the canonical project context; the root `AGENTS.md`, `CLAUDE.md`, and `GEMINI.md` are minimal bootstraps that point here and do nothing else.
- There is **no local AI corpus and no generated tool adapters** in this repository. Shared skills arrive through the Cratis AI marketplace plugins (Claude Code, Codex, GitHub Copilot, Cursor, and Pi are installable today — see the [harness guide](https://www.cratis.io/ai/harnesses/)).

For contributors:

1. Install the Cratis plugin for your harness once (per the harness guide); the subscribed profiles' skills then load automatically when tasks match.
2. General, reusable improvements are proposed in [`Cratis/AI`](https://github.com/Cratis/AI) — never copied into, or synchronized from, this repository.
3. Repository-specific facts and conventions belong in this file; repository-local skills live under `.agents/skills/`.
4. AI session work records (plans, handovers, session notes, scratch analyses) stay in the untracked `.ai-work/` folder and never enter git; a durable follow-up becomes a GitHub issue.
