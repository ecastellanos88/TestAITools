# Patient Service Demo

A demonstration project showcasing **Clean Architecture** with **AI-Assisted Development** using agentic workflows.

## 🏗️ Architecture

This project follows Clean Architecture principles with clear separation of concerns:

- **Domain Layer**: Core business entities and rules
- **Application Layer**: Use cases and business logic (CQRS pattern)
- **Infrastructure Layer**: Data access and external services
- **API Layer**: HTTP endpoints and presentation

See [architecture-rules.md](architecture-rules.md) for detailed architecture guidelines.

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Your favorite IDE (Visual Studio, VS Code, Rider)

### Running the Application

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the API
dotnet run --project src/PatientService.API

# The API will be available at:
# https://localhost:5001
# http://localhost:5000
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run architecture tests
dotnet test tests/PatientService.ArchTests

# Run unit tests
dotnet test tests/PatientService.UnitTests
```

## 📚 API Documentation

Once the application is running, navigate to:
- Swagger UI: `https://localhost:5001/swagger`

### Available Endpoints

#### Patients
- `POST /api/patients` - Create new patient

### Example Request

```bash
# Create a patient
curl -X POST https://localhost:5001/api/patients \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "John",
    "lastName": "Doe",
    "dateOfBirth": "1990-01-01",
    "email": "john.doe@example.com",
    "phoneNumber": "+1234567890",
    "address": "123 Main St"
  }'
```

## 🤖 AI Agents

This project uses AI agents to assist with development:

- **[Architecture Agent](ai-agents/architecture-agent.md)**: Ensures Clean Architecture compliance
- **[Test Agent](ai-agents/test-agent.md)**: Maintains test coverage and quality
- **[Security Agent](ai-agents/security-agent.md)**: Reviews security best practices
- **[PR Review Agent](ai-agents/pr-review-agent.md)**: Automated code review guidance

## 🏛️ Project Structure

```
patient-service-demo/
├── src/
│   ├── PatientService.API          # HTTP API layer
│   ├── PatientService.Application  # Business logic (CQRS)
│   ├── PatientService.Domain       # Core entities
│   └── PatientService.Infrastructure # Data access
├── tests/
│   ├── PatientService.UnitTests    # Unit tests
│   └── PatientService.ArchTests    # Architecture tests
├── ai-agents/                      # AI agent documentation
└── .github/workflows/              # CI/CD pipelines
```

## 🧪 Testing Strategy

### Architecture Tests
Automated tests that enforce architectural rules:
- Layer dependency validation
- Naming convention enforcement
- Namespace organization

### Unit Tests
Test business logic in isolation:
- Handler logic
- Repository operations
- Domain entity behavior

## 🔒 Security

See [security-agent.md](ai-agents/security-agent.md) for security guidelines.

Key security features:
- Input validation
- HTTPS enforcement
- Secure configuration management

## 🛠️ Technologies

- **.NET 8**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **NetArchTest**: Architecture testing

## 📝 Contributing

1. Follow the architecture rules in [architecture-rules.md](architecture-rules.md)
2. Ensure all tests pass
3. Review AI agent guidelines before submitting PR
4. Use the PR template for all pull requests

## 📄 License

This is a demo project for educational purposes.

