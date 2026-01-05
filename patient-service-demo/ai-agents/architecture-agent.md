# Architecture Agent

## Purpose
This agent ensures that the codebase adheres to Clean Architecture principles and maintains proper separation of concerns.

## Responsibilities

### 1. Layer Dependency Validation
- **Domain Layer**: Must not depend on any other layer
- **Application Layer**: Can only depend on Domain layer
- **Infrastructure Layer**: Can depend on Domain and Application layers
- **API Layer**: Can depend on all other layers

### 2. Naming Conventions
- Controllers must end with `Controller`
- Handlers must end with `Handler`
- Commands must end with `Command`
- Repositories must end with `Repository`

### 3. Architecture Rules
- Domain entities should be in `PatientService.Domain` namespace
- Application logic should use CQRS pattern
- Infrastructure should implement interfaces defined in Application
- API controllers should be thin and delegate to handlers

### 4. Code Review Checklist
- [ ] No circular dependencies between layers
- [ ] Domain layer is pure (no external dependencies)
- [ ] Application layer defines interfaces for infrastructure
- [ ] Controllers only orchestrate, no business logic
- [ ] Proper use of dependency injection

## Automated Checks
Run architecture tests with:
```bash
dotnet test tests/PatientService.ArchTests
```

## Violations
Any violation of these rules should be flagged in PR reviews and must be fixed before merging.

