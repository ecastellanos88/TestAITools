# AI Agents - Patient Management System

## 📋 Overview

This directory contains AI agent rules and standards for the Patient Management System. These agents enforce best practices, architectural patterns, and code quality standards.

---

## 🤖 Available Agents

### 1. **Architecture Agent** 
**File**: `architecture-agent.md`

Enforces backend architecture rules:
- ✅ Clean Architecture (4 layers)
- ✅ CQRS Pattern
- ✅ Domain-Driven Design
- ✅ Dependency Injection
- ✅ Repository Pattern
- ✅ Separation of Concerns

**Use for**: Backend .NET development

---

### 2. **Frontend Agent** ⭐ NEW
**File**: `frontend-agent.md` (980 lines)

Enforces frontend development standards:
- ✅ Angular 18.2.x (locked version)
- ✅ Corporate design system
- ✅ Standalone components
- ✅ Reactive forms
- ✅ RxJS patterns
- ✅ Testing standards (80% coverage)
- ✅ Accessibility (WCAG 2.1 AA)
- ✅ Performance optimization
- ✅ Security best practices

**Use for**: Angular frontend development

---

### 3. **Test Agent**
**File**: `test-agent.md`

Enforces testing standards:
- ✅ Unit testing requirements
- ✅ AAA pattern (Arrange-Act-Assert)
- ✅ Test coverage minimums
- ✅ Naming conventions
- ✅ Mock/stub patterns

**Use for**: Writing tests (backend & frontend)

---

### 4. **Security Agent**
**File**: `security-agent.md`

Enforces security best practices:
- ✅ Input validation
- ✅ Authentication/Authorization
- ✅ Data protection
- ✅ OWASP Top 10
- ✅ Secure coding practices

**Use for**: Security reviews and implementation

---

### 5. **PR Review Agent**
**File**: `pr-review-agent.md`

Enforces code review standards:
- ✅ Code quality checks
- ✅ Architecture compliance
- ✅ Test coverage verification
- ✅ Documentation requirements
- ✅ Security review

**Use for**: Pull request reviews

---

## 📚 How to Use These Agents

### For Developers

1. **Before starting a task**: Read the relevant agent file
2. **During development**: Follow the rules strictly
3. **Before committing**: Verify compliance with agent rules
4. **During code review**: Use agents as checklist

### For AI Assistants

1. **ALWAYS** read and follow the relevant agent rules
2. **NEVER** deviate from agent standards without explicit approval
3. **VALIDATE** all code against agent requirements
4. **REFERENCE** specific agent sections when explaining decisions

### For Code Reviewers

1. Use agents as review checklist
2. Verify compliance with all applicable agents
3. Reference specific agent rules in review comments
4. Ensure tests meet agent standards

---

## 🎯 Agent Priority

When multiple agents apply, follow this priority:

1. **Security Agent** - Security is paramount
2. **Architecture Agent** - Maintain architectural integrity
3. **Frontend/Backend Agent** - Follow technology-specific rules
4. **Test Agent** - Ensure quality through testing
5. **PR Review Agent** - Final quality gate

---

## 📖 Quick Reference

| Agent | Primary Focus | File Size | Key Rules |
|-------|---------------|-----------|-----------|
| Architecture | Backend structure | ~300 lines | Clean Architecture, CQRS |
| Frontend | Angular development | 980 lines | Angular 18.2.x, Design System |
| Test | Testing standards | ~200 lines | 80% coverage, AAA pattern |
| Security | Security practices | ~200 lines | Input validation, OWASP |
| PR Review | Code review | ~150 lines | Quality gates |

---

## 🔄 Updating Agents

### When to Update

- New technology versions adopted
- New patterns/practices introduced
- Security vulnerabilities discovered
- Team feedback on rules

### How to Update

1. Discuss changes with team
2. Update relevant agent file
3. Update version number
4. Communicate changes to team
5. Update related documentation

---

## 📞 Support

**Questions about agents?**
1. Read the specific agent file thoroughly
2. Check examples in the codebase
3. Consult with tech lead
4. Discuss in team meetings

**Found an issue?**
1. Document the problem
2. Propose a solution
3. Discuss with team
4. Update agent if approved

---

## ✅ Compliance Checklist

Before merging code, verify:

- [ ] Follows Architecture Agent rules (backend)
- [ ] Follows Frontend Agent rules (frontend)
- [ ] Meets Test Agent standards
- [ ] Passes Security Agent review
- [ ] Complies with PR Review Agent

---

## 📊 Agent Statistics

| Metric | Value |
|--------|-------|
| Total Agents | 5 |
| Total Lines | ~1,830 |
| Coverage | Backend + Frontend + Testing + Security + Review |
| Last Updated | 2024-01-20 |

---

**Maintained By**: Development Team  
**Version**: 1.0.0  
**Status**: ✅ Active

