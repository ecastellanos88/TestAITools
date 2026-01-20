# Patient Service Demo

A full-stack **Patient Management Portal** showcasing **Clean Architecture** with **AI-Assisted Development** using agentic workflows.

## 🎯 What's This?

A complete healthcare application with:
- ✅ **.NET 9 Backend** (Clean Architecture + CQRS)
- ✅ **Angular 18 Frontend** (Standalone components + Reactive forms)
- ✅ **Azure Deployment** (Free tier - $0/month)
- ✅ **CI/CD Pipeline** (GitHub Actions)
- ✅ **13 Passing Tests** (Backend + Frontend)

**Live Demo**: Deploy to Azure in 15 minutes! See [AZURE-DEPLOYMENT-GUIDE.md](AZURE-DEPLOYMENT-GUIDE.md)

---

## 🏗️ Architecture

This project follows Clean Architecture principles with clear separation of concerns:

### Backend (.NET 9)
- **Domain Layer**: Core business entities and rules
- **Application Layer**: Use cases and business logic (CQRS pattern)
- **Infrastructure Layer**: Data access and external services
- **API Layer**: HTTP endpoints and presentation

### Frontend (Angular 18)
- **Standalone Components**: Modern Angular architecture
- **Reactive Forms**: Type-safe form handling
- **RxJS Patterns**: Observable streams and error handling
- **Corporate Design System**: Consistent UI/UX

See [architecture-rules.md](architecture-rules.md) for detailed architecture guidelines.

## 🚀 Quick Start

### Option 1: Run Locally (5 minutes)

**Prerequisites**:
- .NET 9 SDK
- Node.js 20.x
- Your favorite IDE (Visual Studio, VS Code, Rider)

**Start Everything**:
```powershell
# Windows - Start both backend and frontend
.\start-all.ps1

# Backend will be at: http://localhost:5000
# Frontend will be at: http://localhost:4200
```

**Manual Start**:
```bash
# Backend (.NET 9)
cd src/PatientService.API
dotnet run

# Frontend (Angular 18) - in new terminal
cd patient-portal
npm install
npm start
```

**Detailed Guide**: See [HOW-TO-RUN.md](HOW-TO-RUN.md)

---

### Option 2: Deploy to Azure (15 minutes) ☁️

**Deploy for FREE** using Azure's free tier:

```powershell
# 1. Deploy backend
.\deploy-backend-azure.ps1

# 2. Deploy frontend
.\deploy-frontend-azure.ps1 -BackendUrl "https://your-api.azurewebsites.net"
```

**Cost**: $0/month (Free tier)
**Guide**: See [AZURE-DEPLOYMENT-GUIDE.md](AZURE-DEPLOYMENT-GUIDE.md)
**Summary**: See [AZURE-DEPLOYMENT-SUMMARY.md](AZURE-DEPLOYMENT-SUMMARY.md)

---

### Running Tests

```bash
# Backend tests (13 tests)
dotnet test

# Frontend tests (8 tests)
cd patient-portal
npm test

# Architecture tests
dotnet test tests/PatientService.ArchTests

# Unit tests only
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

### Backend Agents
- **[Architecture Agent](ai-agents/architecture-agent.md)**: Ensures Clean Architecture compliance
- **[Test Agent](ai-agents/test-agent.md)**: Maintains test coverage and quality
- **[Security Agent](ai-agents/security-agent.md)**: Reviews security best practices
- **[PR Review Agent](ai-agents/pr-review-agent.md)**: Automated code review guidance

### Frontend Agent
- **[Frontend Agent](ai-agents/frontend-agent.md)**: Complete Angular 18 guidelines (980 lines)
  - Mandatory Angular 18.2.x version
  - Corporate design system
  - Component patterns and best practices
  - Testing requirements (80% coverage)
  - Security and accessibility standards

## 🏛️ Project Structure

```
patient-service-demo/
├── src/                                    # Backend (.NET 9)
│   ├── PatientService.API                  # HTTP API layer
│   ├── PatientService.Application          # Business logic (CQRS)
│   ├── PatientService.Domain               # Core entities
│   └── PatientService.Infrastructure       # Data access
├── patient-portal/                         # Frontend (Angular 18)
│   ├── src/app/components/                 # UI components
│   ├── src/app/services/                   # HTTP services
│   └── src/environments/                   # Environment configs
├── tests/                                  # Backend tests
│   ├── PatientService.UnitTests            # Unit tests (13 tests)
│   └── PatientService.ArchTests            # Architecture tests
├── ai-agents/                              # AI agent documentation
│   ├── architecture-agent.md               # Backend architecture rules
│   ├── frontend-agent.md                   # Frontend development rules
│   ├── test-agent.md                       # Testing standards
│   └── security-agent.md                   # Security guidelines
├── .github/workflows/                      # CI/CD pipelines
│   └── azure-deploy.yml                    # Azure deployment workflow
├── deploy-backend-azure.ps1                # Backend deployment script
├── deploy-frontend-azure.ps1               # Frontend deployment script
├── AZURE-DEPLOYMENT-GUIDE.md               # Complete Azure guide
├── AZURE-CICD-SETUP.md                     # CI/CD setup guide
└── AZURE-FREE-TIER-LIMITS.md               # Cost management guide
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

### Backend Stack
- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core**: Web API framework
- **CQRS Pattern**: Command Query Responsibility Segregation
- **Clean Architecture**: 4-layer architecture
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **NetArchTest**: Architecture testing

### Frontend Stack
- **Angular 18.2.0**: Latest Angular (locked version)
- **TypeScript 5.5.x**: Type-safe JavaScript
- **RxJS 7.8.0**: Reactive programming
- **Standalone Components**: Modern Angular architecture
- **Reactive Forms**: Type-safe form handling
- **Jasmine + Karma**: Testing framework

### Cloud & DevOps
- **Azure App Service**: Backend hosting (Free F1)
- **Azure Static Web Apps**: Frontend hosting (Free)
- **GitHub Actions**: CI/CD automation
- **PowerShell**: Deployment scripts

## ☁️ Azure Deployment

### Free Tier Deployment ($0/month)

Deploy the entire application to Azure for **FREE**:

**Quick Start**:
1. Create Azure free account: https://azure.microsoft.com/free
2. Run deployment scripts:
   ```powershell
   .\deploy-backend-azure.ps1
   .\deploy-frontend-azure.ps1 -BackendUrl "https://your-api.azurewebsites.net"
   ```
3. Your app is live! 🎉

**Documentation**:
- 📖 **[AZURE-DEPLOYMENT-SUMMARY.md](AZURE-DEPLOYMENT-SUMMARY.md)** - Executive summary
- 📖 **[AZURE-DEPLOYMENT-GUIDE.md](AZURE-DEPLOYMENT-GUIDE.md)** - Complete guide (350 lines)
- 📖 **[AZURE-CICD-SETUP.md](AZURE-CICD-SETUP.md)** - CI/CD automation
- 📖 **[AZURE-FREE-TIER-LIMITS.md](AZURE-FREE-TIER-LIMITS.md)** - Cost management

**What You Get**:
- ✅ Backend: Azure App Service (Free F1)
- ✅ Frontend: Azure Static Web Apps (Free)
- ✅ SSL/HTTPS: Included automatically
- ✅ CI/CD: GitHub Actions (Free)
- ✅ Total Cost: **$0/month**

---

## 📚 Documentation

### Getting Started
- [HOW-TO-RUN.md](HOW-TO-RUN.md) - Complete execution guide
- [MANUAL-EXECUTION.md](MANUAL-EXECUTION.md) - Step-by-step manual
- [DOCUMENTATION-INDEX.md](DOCUMENTATION-INDEX.md) - Complete documentation index

### Azure Deployment
- [AZURE-DEPLOYMENT-SUMMARY.md](AZURE-DEPLOYMENT-SUMMARY.md) - Quick overview
- [AZURE-DEPLOYMENT-GUIDE.md](AZURE-DEPLOYMENT-GUIDE.md) - Detailed deployment steps
- [AZURE-CICD-SETUP.md](AZURE-CICD-SETUP.md) - CI/CD configuration
- [AZURE-FREE-TIER-LIMITS.md](AZURE-FREE-TIER-LIMITS.md) - Costs and limitations

### Architecture & Development
- [architecture-rules.md](architecture-rules.md) - Backend architecture rules
- [ai-agents/frontend-agent.md](ai-agents/frontend-agent.md) - Frontend development rules
- [PATIENT-LIST-IMPLEMENTATION.md](PATIENT-LIST-IMPLEMENTATION.md) - Feature implementation guide

---

## 📝 Contributing

1. Follow the architecture rules in [architecture-rules.md](architecture-rules.md)
2. Follow frontend rules in [ai-agents/frontend-agent.md](ai-agents/frontend-agent.md)
3. Ensure all tests pass (backend + frontend)
4. Review AI agent guidelines before submitting PR
5. Use the PR template for all pull requests

---

## 📄 License

This is a demo project for educational purposes.

---

## 🎯 Features

### Current Features ✅
- ✅ Patient creation with validation
- ✅ Patient list with responsive grid
- ✅ Age calculation from date of birth
- ✅ Email and phone formatting
- ✅ Loading states and error handling
- ✅ Corporate design system
- ✅ Responsive design (desktop/tablet/mobile)
- ✅ 13 backend tests passing
- ✅ 8 frontend tests passing

### Deployment Features ✅
- ✅ Azure deployment scripts
- ✅ CI/CD with GitHub Actions
- ✅ Free tier deployment ($0/month)
- ✅ SSL/HTTPS included
- ✅ Global CDN for frontend

### Coming Soon 🚧
- 🚧 Patient editing
- 🚧 Patient deletion
- 🚧 Search and filtering
- 🚧 Authentication (Azure AD B2C)
- 🚧 Database integration (Azure SQL/Cosmos DB)
- 🚧 Application Insights monitoring

---

**Ready to get started?** 🚀

1. **Run Locally**: See [HOW-TO-RUN.md](HOW-TO-RUN.md)
2. **Deploy to Azure**: See [AZURE-DEPLOYMENT-SUMMARY.md](AZURE-DEPLOYMENT-SUMMARY.md)
3. **Learn Architecture**: See [architecture-rules.md](architecture-rules.md)

