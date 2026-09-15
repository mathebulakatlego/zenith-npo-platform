# Zenith NPO Platform — Student & Technical Learning Notes

## 1. Purpose

This document records the main technical concepts learned while building the Zenith NPO Platform.

The goal is not to document every line of code, but to capture the important engineering decisions, implementation patterns, debugging lessons and principles that can be explained in an interview.

---

## 2. Current Architecture

```text
React Frontend
      ↓
ASP.NET Core Web API
      ↓
Entity Framework Core
      ↓
SQLite Database
```

The project uses a simple layered structure suitable for a small portfolio application.

---

## 3. Backend Structure

```text
Controllers/ → HTTP/API endpoints
DTOs/        → API request/response contracts
Models/      → Domain/database entities
Data/        → EF Core DbContext
Services/    → Business logic
```

The separation keeps responsibilities clear without introducing unnecessary architectural complexity.

---

## 4. DbContext and Database

`ZenithDbContext` is the EF Core database context.

It provides `DbSet<T>` properties for the main entities and allows the application to query and persist data.

SQLite is used because it is lightweight and appropriate for the project's development and portfolio scope.

---

## 5. Entity Relationships

The main relationships follow the approved domain model:

```text
Applicant → Application
Programme → Application
Application → Learner
Programme → Cohort
Programme → Module
Cohort → Learner
Learner → Progress
Learner → Result
Module → Progress
Module → Result
```

The database design was created before implementation so that the code follows the agreed business model.

---

## 6. EF Core Migrations

EF Core migrations are used to keep the database schema aligned with the entity models.

Typical workflow:

```text
Change model
    ↓
Create migration
    ↓
Apply migration
    ↓
Test database
```

Migrations provide a repeatable way to track schema changes instead of manually modifying the database.

---

## 7. `.csproj` and Project Structure

The project uses a slightly unusual folder structure where some backend folders sit outside `backend/Zenith.Api`.

The `.csproj` therefore explicitly links those files into the project.

### Important lesson

A file existing in the repository does not necessarily mean that the .NET project is compiling it.

When a new DTO caused build errors, the issue was traced to the missing `<Compile Include="..."/>` entry in the existing `<ItemGroup>`.

**Debugging principle:**

```text
File exists
      ≠
Project includes file
```

Check both the physical file and the `.csproj` when something unexpectedly cannot be found.

---

## 8. Program.cs

`Program.cs` configures the application.

It currently registers:

* EF Core / SQLite
* Controllers
* Swagger/OpenAPI
* Graduation eligibility service
* ProblemDetails

It also configures the HTTP request pipeline and maps controllers.

---

## 9. Controllers and Endpoints

Controllers expose REST API endpoints.

Examples:

```text
GET    /api/programmes
POST   /api/programmes

GET    /api/applicants
POST   /api/applicants

GET    /api/applications
POST   /api/applications

GET    /api/learners
POST   /api/learners

GET    /api/learners/{id}/eligibility
```

Controllers handle HTTP concerns and basic database/business-rule checks.

---

## 10. DTOs

DTOs (Data Transfer Objects) define what the API accepts or returns instead of always exposing database entities directly.

### Create DTO

Used for incoming data:

```text
Client request
     ↓
Create DTO
     ↓
Controller
     ↓
Entity
     ↓
Database
```

### Response DTO

Used where returning the entity directly could expose navigation properties or unnecessary internal structure.

### Why use DTOs?

* Separates API contracts from database models
* Controls what clients can submit
* Controls what the API returns
* Allows validation at the API boundary
* Makes future model changes safer

DTOs are used **selectively**, not automatically for every endpoint.

For a small project, manual mapping is sufficient:

```csharp
var programme = new Programme
{
    Name = dto.Name,
    Description = dto.Description,
    IsActive = dto.IsActive
};
```

AutoMapper was deliberately not introduced because the project is small and explicit mapping is easy to understand.

---

## 11. API Validation

Validation is split between request validation and business validation.

### DTO validation

DataAnnotations handle rules such as:

```text
Required
StringLength
EmailAddress
Range
```

For example, marks and progress percentages are restricted to `0–100`.

ASP.NET Core's `[ApiController]` behavior automatically returns a `400 Bad Request` when model validation fails.

### Business validation

Rules requiring database knowledge remain in controllers/services.

Examples:

```text
Does the Programme exist?
Does the Learner exist?
Is the Application approved?
Is the Progress record a duplicate?
Is EndDate after StartDate?
```

This separation keeps DTOs focused on the API contract and business logic focused on application rules.

---

## 12. API Error Handling

The API uses built-in ASP.NET Core `ProblemDetails` for structured errors.

The main response categories are:

```text
400 → Invalid request / business validation
404 → Resource not found
409 → Conflict / duplicate
500 → Unexpected server error
```

Unexpected exceptions are handled through the application's exception handler rather than creating a custom exception framework.

The principle is:

> Use the framework's standard capabilities before building custom infrastructure.

---

## 13. HTTP Status Codes

The API uses status codes according to the result of the operation.

| Status | Meaning                              |
| ------ | ------------------------------------ |
| 200    | Successful request                   |
| 201    | Resource successfully created        |
| 400    | Invalid request                      |
| 404    | Resource does not exist              |
| 409    | Request conflicts with existing data |
| 500    | Unexpected server error              |

This makes the API easier for a frontend client to consume predictably.

---

## 14. EF Core Querying

The application uses LINQ with EF Core.

Examples of operations include:

```text
Where()
FirstOrDefaultAsync()
CountAsync()
AverageAsync()
Include()
ToListAsync()
```

Asynchronous database operations are used where appropriate.

The important principle is to keep database querying in the data-access/application layer rather than mixing database concerns into unrelated code.

---

## 15. Business Logic

Graduation eligibility is handled by:

`GraduationEligibilityService`

The rule is:

```text
All programme modules completed
        AND
Average mark >= 70%
        ↓
Eligible
```

This logic belongs in a service because it represents a business rule rather than simply an HTTP operation.

---

## 16. Swagger / OpenAPI

Swagger provides an interactive interface for testing the API.

It allows requests to be sent without needing the frontend.

This is useful during backend development because:

```text
Backend
   ↓
Swagger
   ↓
Test endpoint
   ↓
Inspect response
```

The backend can therefore be tested before the React frontend is connected.

---

## 17. API Testing Approach

API testing will be performed systematically rather than testing only whether the application starts.

Each endpoint should be tested for:

1. Successful request
2. Invalid input
3. Missing resource
4. Duplicate/conflicting data where applicable
5. Business-rule failure where applicable

The objective is to verify both the **happy path** and expected failure behaviour.

---

## 18. Debugging Lessons

Important lessons from implementation:

### A file can exist but not be part of the project

Always check the `.csproj` when compilation behaves unexpectedly.

### Code can appear correct but not be physically saved

When changes do not appear in Swagger or Git:

```text
Check physical file
    ↓
Check project inclusion
    ↓
Build
    ↓
Run application
    ↓
Check Swagger
```

Do not assume the intended code was actually saved.

### Diagnose before changing

When a build or runtime problem occurs:

```text
Observe error
→ Identify cause
→ Make smallest correction
→ Build
→ Test
```

Avoid adding more code simply because something failed.

---

## 19. Designed vs Implemented vs Tested

These are treated as separate states.

**Designed**
The requirement or technical solution has been agreed.

**Configured**
The project contains the necessary framework/package/configuration support.

**Implemented**
The feature exists in the application code.

**Tested**
The implementation has been exercised and the expected result verified.

**Completed**
Implemented, tested and committed.

This distinction prevents unfinished work from being treated as complete.

---

## 20. Current Learning Position

The backend has progressed through:

```text
Requirements
    ↓
Architecture
    ↓
Database design
    ↓
EF Core + SQLite
    ↓
Entities
    ↓
Controllers
    ↓
Business logic
    ↓
DTOs
    ↓
Validation
    ↓
Error handling
    ↓
API testing  ← CURRENT
    ↓
Frontend
```

The current focus is systematic API testing before moving into frontend implementation.

---

## 21. Interview Mental Model

The overall backend can be explained as:

> I started with the business requirements and domain model, designed the relationships, implemented the entities with EF Core and SQLite, exposed them through REST controllers, introduced DTOs to separate API contracts from database models, added request and business validation, standardised API error responses, and then tested the endpoints through Swagger before connecting the frontend.

The important principle throughout the project is:

> **Understand the requirement → design the change → implement the smallest useful solution → test it → document it → commit it.**