# Documentation Index - Patient Management System

## 📚 Complete Documentation Guide

This document provides an index of all documentation files in the project.

---

## 🎯 Architecture & Rules

### Backend Architecture

| File | Description | Lines | Purpose |
|------|-------------|-------|---------|
| `architecture-rules.md` | Backend architecture rules | ~500 | Clean Architecture, CQRS, DDD |
| `ai-agents/architecture-agent.md` | Architecture enforcement agent | ~300 | Enforce backend patterns |
| `ai-agents/test-agent.md` | Testing standards agent | ~200 | Unit test requirements |
| `ai-agents/security-agent.md` | Security standards agent | ~200 | Security best practices |

### Frontend Architecture

| File | Description | Lines | Purpose |
|------|-------------|-------|---------|
| `patient-service-demo/ai-agents/frontend-agent.md` | **Frontend rules & standards** | **980** | **Complete Angular guidelines** |
| `patient-portal/FRONTEND-GUIDELINES.md` | Quick reference guide | 150 | Daily development reference |
| `patient-portal/README.md` | Project overview | 279 | Getting started guide |
| `patient-portal/.eslintrc.json` | Linting configuration | 60 | Code quality enforcement |

---

## 🚀 Execution Guides

### Getting Started

| File | Description | Language | Audience |
|------|-------------|----------|----------|
| `MANUAL-EXECUTION.md` | Manual execution guide | English | All users |
| `HOW-TO-RUN.md` | Complete execution guide | English | All users |
| `start-backend.bat` | Backend startup script | Batch | Windows users |
| `start-frontend.bat` | Frontend startup script | Batch | Windows users |
| `start-all.ps1` | Full system startup | PowerShell | Windows users |

---

## ☁️ Azure Deployment

### Deployment Documentation

| File | Description | Lines | Purpose |
|------|-------------|-------|---------|
| `AZURE-DEPLOYMENT-GUIDE.md` | **Complete Azure deployment guide** | **350** | **Step-by-step Azure setup** |
| `AZURE-CICD-SETUP.md` | CI/CD configuration guide | 200 | GitHub Actions automation |
| `AZURE-FREE-TIER-LIMITS.md` | Free tier costs & limitations | 250 | Cost management & optimization |

### Deployment Scripts

| File | Description | Platform | Purpose |
|------|-------------|----------|---------|
| `deploy-backend-azure.ps1` | Backend deployment script | PowerShell | Deploy .NET API to App Service |
| `deploy-frontend-azure.ps1` | Frontend deployment script | PowerShell | Deploy Angular to Static Web Apps |
| `.github/workflows/azure-deploy.yml` | CI/CD workflow | GitHub Actions | Automated deployment pipeline |

### Azure Services Used

| Service | Tier | Cost | Purpose |
|---------|------|------|---------|
| **Azure App Service** | Free F1 | $0/month | .NET 9 API hosting |
| **Azure Static Web Apps** | Free | $0/month | Angular 18 hosting |
| **GitHub Actions** | Free | $0/month | CI/CD automation |

---

## 📖 Summary Documents

| File | Description | Purpose |
|------|-------------|---------|
| `FRONTEND-AGENT-SUMMARY.md` | Frontend agent overview | Quick understanding of frontend rules |
| `README-FRONTEND.md` | Full-stack documentation | Complete system overview |
| `DOCUMENTATION-INDEX.md` | This file | Navigate all documentation |

---

## 🗂️ Documentation by Role

### For New Developers

**Start here** (in order):

1. `README-FRONTEND.md` - System overview
2. `MANUAL-EXECUTION.md` - How to run the app
3. `patient-portal/README.md` - Frontend project overview
4. `patient-portal/FRONTEND-GUIDELINES.md` - Quick reference
5. `ai-agents/frontend-agent.md` - Complete rules

### For Frontend Developers

**Daily use**:

1. `patient-portal/FRONTEND-GUIDELINES.md` - Quick templates
2. `ai-agents/frontend-agent.md` - Complete rules
3. `patient-portal/README.md` - Commands reference

**Reference**:
- Corporate colors: `ai-agents/frontend-agent.md` (lines 22-31)
- Component template: `FRONTEND-GUIDELINES.md` (lines 40-70)
- Service template: `FRONTEND-GUIDELINES.md` (lines 75-95)
- CSS template: `FRONTEND-GUIDELINES.md` (lines 100-135)

### For Backend Developers

**Daily use**:

1. `architecture-rules.md` - Architecture patterns
2. `ai-agents/architecture-agent.md` - Enforcement rules
3. `ai-agents/test-agent.md` - Testing standards
4. `ai-agents/security-agent.md` - Security rules

### For DevOps/Deployment

**Reference**:

1. `HOW-TO-RUN.md` - Execution instructions
2. `patient-portal/README.md` - Build & deployment
3. `README-FRONTEND.md` - System architecture

### For AI Agents

**Must follow**:

1. `ai-agents/frontend-agent.md` - Frontend rules (STRICT)
2. `ai-agents/architecture-agent.md` - Backend rules (STRICT)
3. `ai-agents/test-agent.md` - Testing rules (STRICT)
4. `ai-agents/security-agent.md` - Security rules (STRICT)

---

## 📂 File Locations

### Root Directory (`patient-service-demo/`)

```
patient-service-demo/
├── HOW-TO-RUN.md                       # Execution guide
├── MANUAL-EXECUTION.md                 # Manual execution instructions
├── FRONTEND-AGENT-SUMMARY.md           # Frontend agent summary
├── README-FRONTEND.md                  # Full-stack documentation
├── DOCUMENTATION-INDEX.md              # This file
├── start-backend.bat                   # Backend script
├── start-frontend.bat                  # Frontend script
└── start-all.ps1                       # Full system script
```

### AI Agents Directory (`patient-service-demo/ai-agents/`)

```
patient-service-demo/ai-agents/
├── architecture-agent.md               # Backend architecture rules
├── frontend-agent.md                   # Frontend rules (980 lines)
├── pr-review-agent.md                  # PR review standards
├── test-agent.md                       # Testing standards
└── security-agent.md                   # Security standards
```

### Frontend Directory (`patient-portal/`)

```
patient-portal/
├── README.md                           # Project overview
├── FRONTEND-GUIDELINES.md              # Quick reference
├── .eslintrc.json                      # Linting config
├── angular.json                        # Angular config
├── package.json                        # Dependencies
└── src/
    ├── app/
    │   ├── components/
    │   ├── models/
    │   ├── services/
    │   └── environments/
    └── styles.css                      # Global styles
```

---

## 🔍 Quick Search

### Find Information About...

**Angular Version**:
- `ai-agents/frontend-agent.md` (line 13)
- `patient-portal/package.json` (line 14)

**Corporate Colors**:
- `ai-agents/frontend-agent.md` (lines 22-31)
- `patient-portal/src/styles.css` (lines 22-31)

**Component Template**:
- `FRONTEND-GUIDELINES.md` (lines 40-70)
- `ai-agents/frontend-agent.md` (lines 138-150)

**Service Template**:
- `FRONTEND-GUIDELINES.md` (lines 75-95)
- `ai-agents/frontend-agent.md` (lines 280-310)

**Form Validation**:
- `ai-agents/frontend-agent.md` (lines 177-250)
- `FRONTEND-GUIDELINES.md` (lines 40-70)

**Testing Standards**:
- `ai-agents/frontend-agent.md` (lines 430-480)
- `ai-agents/test-agent.md`

**Execution Instructions**:
- `MANUAL-EXECUTION.md` (complete guide)
- `HOW-TO-RUN.md` (troubleshooting)

---

## 📊 Documentation Statistics

| Category | Files | Total Lines | Purpose |
|----------|-------|-------------|---------|
| Frontend Rules | 4 | ~1,468 | Angular standards |
| Backend Rules | 4 | ~1,200 | .NET standards |
| Execution Guides | 5 | ~800 | How to run |
| Summaries | 3 | ~500 | Quick overview |
| **Total** | **16** | **~3,968** | **Complete system** |

---

## ✅ Documentation Checklist

When creating new features, update:

- [ ] Relevant agent file (frontend-agent.md or architecture-agent.md)
- [ ] Quick reference guide (FRONTEND-GUIDELINES.md)
- [ ] Project README (patient-portal/README.md)
- [ ] This index (if new files created)

---

## 🔄 Version History

| Version | Date | Changes |
|---------|------|---------|
| 1.0.0 | 2024-01-20 | Initial documentation creation |
| | | - Frontend agent (979 lines) |
| | | - Quick reference guide |
| | | - Execution guides |
| | | - Summary documents |

---

## 📞 Support

**Can't find what you need?**

1. Use Ctrl+F to search this index
2. Check the "Quick Search" section above
3. Review the "Documentation by Role" section
4. Contact development team

---

**Last Updated**: 2024-01-20  
**Maintained By**: Development Team  
**Status**: ✅ Active

