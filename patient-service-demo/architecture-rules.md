# Architecture Rules

## Overview
This project follows **Clean Architecture** principles to ensure maintainability, testability, and separation of concerns.

## Layer Structure

```
┌─────────────────────────────────────┐
│         API Layer                   │
│  (Controllers, Middleware)          │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│      Application Layer              │
│  (Use Cases, Handlers, Commands)    │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│    Infrastructure Layer             │
│  (Repositories, External Services)  │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│       Domain Layer                  │
│  (Entities, Value Objects)          │
└─────────────────────────────────────┘
```

## Dependency Rules

### 1. Domain Layer
- **Location**: `src/PatientService.Domain`
- **Dependencies**: NONE
- **Purpose**: Contains business entities and core business rules
- **Rules**:
  - No dependencies on other layers
  - No framework dependencies
  - Pure C# classes
  - Contains only business logic

### 2. Application Layer
- **Location**: `src/PatientService.Application`
- **Dependencies**: Domain
- **Purpose**: Contains application business rules and use cases
- **Rules**:
  - Can depend on Domain layer only
  - Defines interfaces for Infrastructure
  - Implements CQRS pattern (Commands/Queries)
  - Contains Handlers for business operations

### 3. Infrastructure Layer
- **Location**: `src/PatientService.Infrastructure`
- **Dependencies**: Domain, Application
- **Purpose**: Implements external concerns (database, file system, etc.)
- **Rules**:
  - Implements interfaces defined in Application
  - Contains repository implementations
  - Handles data persistence
  - External service integrations

### 4. API Layer
- **Location**: `src/PatientService.API`
- **Dependencies**: Application, Infrastructure, Domain
- **Purpose**: HTTP API endpoints and presentation logic
- **Rules**:
  - Controllers should be thin
  - Delegates to Application handlers
  - Handles HTTP concerns only
  - No business logic in controllers

## Design Patterns

### CQRS (Command Query Responsibility Segregation)
- **Commands**: Operations that change state
  - Example: `CreatePatientCommand`
- **Queries**: Operations that return data
  - Example: `GetPatientQuery`
- **Handlers**: Process commands and queries
  - Example: `CreatePatientHandler`

### Repository Pattern
- Abstracts data access
- Defined in Application layer (interface)
- Implemented in Infrastructure layer
- Example: `IPatientRepository` → `InMemoryPatientRepository`

### Dependency Injection
- All dependencies injected via constructor
- Configured in `Program.cs`
- Promotes testability and loose coupling

## Naming Conventions

### Controllers
- Suffix: `Controller`
- Example: `PatientsController`

### Handlers
- Suffix: `Handler`
- Example: `CreatePatientHandler`

### Commands
- Suffix: `Command`
- Example: `CreatePatientCommand`

### Repositories
- Prefix: `I` for interface
- Suffix: `Repository`
- Example: `IPatientRepository`, `InMemoryPatientRepository`

## Testing Strategy

### Architecture Tests
- Validate layer dependencies
- Enforce naming conventions
- Located in `tests/PatientService.ArchTests`

### Unit Tests
- Test business logic in isolation
- Located in `tests/PatientService.UnitTests`

## Enforcement
Architecture rules are enforced through:
1. **Automated Tests**: Architecture tests run on every build
2. **Code Reviews**: PR review agent checks compliance
3. **CI/CD Pipeline**: Fails build if rules are violated

## Benefits
- **Maintainability**: Clear separation of concerns
- **Testability**: Easy to test in isolation
- **Flexibility**: Easy to swap implementations
- **Scalability**: Can grow without becoming messy

