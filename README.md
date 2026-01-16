# Telemetry Platform – ASP.NET Core & PostgreSQL (Dockerized)

This project is a backend telemetry platform built with **ASP.NET Core** and **PostgreSQL**, designed to demonstrate clean backend architecture, REST API design, Docker-based local environments, and database integration using Entity Framework Core.

The application exposes a telemetry ingestion API that accepts device metrics (temperature, CPU usage, timestamps) and stores them in PostgreSQL. It is fully containerized using Docker and Docker Compose.

The project was created as a **portfolio and interview demonstration** and focuses on correctness, clarity, and production-style setup rather than feature bloat.

---

## Architecture Overview

The solution consists of:

- **Telemetry API**  
  ASP.NET Core Web API responsible for:
  - accepting telemetry events
  - validating input
  - persisting data using EF Core
  - exposing query endpoints

- **PostgreSQL**  
  Relational database running in Docker, accessed by the API via an internal Docker network.

### High-level flow

1. Client sends telemetry data via HTTP (REST)
2. API validates and persists data using Entity Framework Core
3. PostgreSQL stores telemetry events
4. API exposes read endpoints with filtering and pagination
5. Health endpoint reports API + database readiness

---

## Technology Stack

- **C#**
- **ASP.NET Core (.NET 8)**
- **Entity Framework Core**
- **PostgreSQL 16**
- **Docker & Docker Compose**
- **Swagger / OpenAPI**
- **Health Checks**

---

## Solution Structure
Telemetry.Api.sln
docker-compose.yml
Telemetry.Api/
├── Controllers/
│ └── TelemetryController.cs
├── Contracts/
│ └── CreateTelemetryRequest.cs
├── Data/
│ ├── TelemetryDbContext.cs
│ └── Migrations/
├── Models/
│ └── TelemetryEvent.cs
├── Program.cs
├── Dockerfile
└── appsettings.json

---

## Prerequisites

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 (recommended)

---

## Running the Project with Docker

### 1. Start containers
From the repository root (where `docker-compose.yml` is located):

```bash
docker compose up -d --build
```

This starts:
- PostgreSQL container
- Telemetry API container

### 2. Apply database migrations
PostgreSQL runs inside Docker with an empty database by default.
Apply EF Core migrations after the containers are up:

```bash
dotnet ef database update --connection "Host=localhost;Port=5432;Database=telemetrydb;Username=app;Password=app"
```

## API Documentation (Swagger)
Swagger is enabled when running in Development mode (including Docker).
Open:

```bash
http://localhost:8080/swagger
```

## API Endpoints
### Create telemetry event
**POST /api/Telemetry** 

```json
{
  "deviceId": "sensor-dresden-01",
  "temperature": 23.4,
  "cpuUsage": 0.71
}
```
Returns:
- 201 Created
- Location header with resource URL

---

### Get telemetry events (paginated)
**GET /api/Telemetry?deviceId=sensor-dresden-01&skip=0&take=100**

---

### Get telemetry event by ID
**GET /api/Telemetry/{id}**

Returns:
- **200** OK if found
- **404 Not Found** if missing

---

## Health check

**GET /health**

Reports:
- API status
- PostgreSQL connectivity
This endpoint is suitable for:
- container orchestration
- readiness probes
- monitoring systems

---

## Reliability & Best Practices Demonstrated
- DTO-based request validation
- Explicit REST status codes (**201 Created**, **404 Not Found**)
- EF Core migrations
- Database indexing for query performance
- Pagination guardrails
- Health checks
- Dockerized local environment
- Configuration via environment variables
- Clean, readable code structure

---

##  Purpose of the Project
This project was created as a backend portfolio example to demonstrate:
- practical ASP.NET Core development
- working with PostgreSQL in Docker
- real-world API design choices
- production-style setup without unnecessary complexity
The focus is on correctness, clarity, and maintainability.

---
## Possible Future Improvements
- Authentication / authorization
- Time-series optimization (e.g. TimescaleDB)
- Background processing
- Structured logging
- CI/CD pipeline
- Kubernetes deployment
- Metrics & tracing
