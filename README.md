# EventFlow Backend

EventFlow Backend is the core service for an event-driven automation platform for developers.

It receives GitHub webhooks (triggers), processes automation workflows, optionally uses AI to analyze and classify issues, and executes actions such as sending notifications to Discord.

---

## What this backend does

- Receives GitHub webhook events (e.g., issue opened)
- Stores the event and execution status
- Processes events asynchronously (worker/background processing)
- Uses AI to summarize/classify issue content (optional in MVP)
- Sends formatted notifications to Discord via webhook

---

## Example workflow (MVP)

1. A GitHub Issue is created
2. GitHub sends a webhook to `POST /webhooks/github`
3. EventFlow stores the event and creates an execution record
4. A worker processes the event
5. EventFlow sends a notification to Discord

---

## Architecture (high level)

GitHub → Webhook Controller → Event Store → Worker → Action Runner → Discord

---

## Tech Stack

- C# / .NET (Web API with Controllers)
- PostgreSQL (data persistence)
- Background processing (Worker / Hosted Service)
- Serilog (structured logging)
- AI (LLM API) for classification/summary (planned)

---

## Solution structure

This repository uses multiple projects to keep responsibilities clear:

- `EventFlow.Api` - Controllers, request/response models, HTTP endpoints
- `EventFlow.Application` - Use cases and orchestration
- `EventFlow.Domain` - Core domain models and business rules
- `EventFlow.Infrastructure` - Persistence, integrations (Discord/GitHub), external services

---

## Planned endpoints (initial)

- `POST /webhooks/github`  
  Receives GitHub webhook events.

- `GET /executions`  
  Lists recent executions (for the frontend to consume).

- `GET /executions/{id}`  
  Shows execution details (event payload, AI output, action result).

---

## Frontend

The frontend repository will provide a simple dashboard to view executions and event details:

- `eventflow-frontend`

---

## Roadmap

- [ ] Webhook receiver (GitHub issue opened)
- [ ] Execution tracking (Succeeded/Failed/Processing)
- [ ] Discord action (send notification)
- [ ] Idempotency (ignore duplicate deliveries)
- [ ] AI analysis for issue summary + priority
- [ ] Basic monitoring endpoints for the frontend