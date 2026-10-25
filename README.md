# TravelDNA — Backend Architecture

![TravelDNA Backend Architecture](backend%20schema.png)

## Tech Stack
- **Frontend:** React + TypeScript
- **Backend:** ASP.NET Core 10 Web API
- **Architecture:** Clean Architecture
- **Database:** PostgreSQL 17
- **Data Access:** Entity Framework Core + Npgsql
- **Containerization:** Docker + Docker Compose
- **API Documentation:** OpenAPI + Scalar

## Architecture Layers
- **API:** HTTP endpoints, authentication, and validation.
- **Application:** Use cases, services, and DTOs.
- **Domain:** Entities and business rules.
- **Infrastructure:** Database access and external integrations.

## Run Locally

Start the backend and database with Docker Compose:

```bash
docker compose up --build
```

- API: `http://localhost:8080`
- API documentation: `http://localhost:8080/scalar`
