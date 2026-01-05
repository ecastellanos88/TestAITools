# Test Agent

## Purpose
This agent ensures comprehensive test coverage and quality of tests across the codebase.

## Responsibilities

### 1. Test Coverage Requirements
- **Unit Tests**: Minimum 80% code coverage
- **Integration Tests**: All API endpoints must have integration tests
- **Architecture Tests**: All architecture rules must be validated

### 2. Test Organization
- Unit tests in `PatientService.UnitTests`
- Architecture tests in `PatientService.ArchTests`
- Follow AAA pattern (Arrange, Act, Assert)

### 3. Test Naming Convention
```
MethodName_Scenario_ExpectedBehavior
```

Example:
```csharp
CreatePatient_WithValidData_ReturnsCreatedPatient()
GetPatient_WithInvalidId_ReturnsNotFound()
```

### 4. What to Test

#### Unit Tests
- Handler logic
- Repository operations
- Domain entity behavior
- Validation logic

#### Architecture Tests
- Layer dependencies
- Naming conventions
- Namespace organization

### 5. Test Quality Checklist
- [ ] Tests are independent and can run in any order
- [ ] Tests have clear arrange, act, assert sections
- [ ] Tests use meaningful names
- [ ] Tests cover happy path and edge cases
- [ ] Tests are fast and don't depend on external resources

## Running Tests
```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/PatientService.UnitTests
dotnet test tests/PatientService.ArchTests

# Run with coverage
dotnet test /p:CollectCoverage=true
```

## PR Requirements
- All tests must pass
- New code must include tests
- Coverage should not decrease

