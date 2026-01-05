# PR Review Agent

## Purpose
This agent provides automated code review guidance for pull requests.

## Responsibilities

### 1. Code Quality Review

#### Code Style
- [ ] Follows C# coding conventions
- [ ] Consistent naming conventions
- [ ] Proper indentation and formatting
- [ ] No commented-out code
- [ ] No TODO comments without tracking

#### Code Complexity
- [ ] Methods are small and focused (< 20 lines ideal)
- [ ] Classes have single responsibility
- [ ] Cyclomatic complexity is reasonable
- [ ] No deep nesting (max 3 levels)

#### Code Duplication
- [ ] No copy-pasted code
- [ ] Common logic extracted to shared methods
- [ ] Proper use of inheritance/composition

### 2. Architecture Review
- [ ] Follows Clean Architecture principles
- [ ] Proper layer separation
- [ ] Dependencies point inward
- [ ] No circular dependencies
- [ ] Interfaces used for abstraction

### 3. Testing Review
- [ ] New code has tests
- [ ] Tests follow AAA pattern
- [ ] Tests are meaningful and not trivial
- [ ] Edge cases are covered
- [ ] Tests are maintainable

### 4. Security Review
- [ ] No security vulnerabilities
- [ ] Input validation present
- [ ] No sensitive data in logs
- [ ] Proper error handling
- [ ] No hardcoded secrets

### 5. Performance Review
- [ ] No obvious performance issues
- [ ] Async/await used properly
- [ ] Database queries are efficient
- [ ] No N+1 query problems
- [ ] Proper use of caching where needed

### 6. Documentation Review
- [ ] Public APIs are documented
- [ ] Complex logic has comments
- [ ] README updated if needed
- [ ] API documentation updated

### 7. Git Hygiene
- [ ] Commit messages are clear
- [ ] No merge commits in feature branch
- [ ] Branch is up to date with main
- [ ] No unnecessary files committed

## Review Process

### Automated Checks
1. Build succeeds
2. All tests pass
3. Architecture tests pass
4. No linting errors
5. Code coverage maintained

### Manual Review
1. Code logic correctness
2. Design decisions
3. User experience impact
4. Breaking changes identified

## PR Template Checklist
```markdown
## Description
[Describe the changes]

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Checklist
- [ ] Code follows project conventions
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] No breaking changes (or documented)
- [ ] Architecture tests pass
- [ ] Security review completed
```

## Approval Criteria
- All automated checks pass
- At least one approval from team member
- No unresolved comments
- Branch is up to date with main

