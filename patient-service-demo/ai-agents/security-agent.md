# Security Agent

## Purpose
This agent ensures security best practices are followed throughout the codebase.

## Responsibilities

### 1. Data Protection
- **PII (Personally Identifiable Information)**: Patient data must be handled securely
- **Encryption**: Sensitive data should be encrypted at rest and in transit
- **Data Validation**: All input must be validated and sanitized

### 2. Authentication & Authorization
- API endpoints should require authentication (when implemented)
- Role-based access control for sensitive operations
- Secure token management

### 3. Security Checklist

#### Input Validation
- [ ] All user inputs are validated
- [ ] SQL injection prevention (parameterized queries)
- [ ] XSS prevention (output encoding)
- [ ] CSRF protection enabled

#### API Security
- [ ] HTTPS enforced in production
- [ ] CORS properly configured
- [ ] Rate limiting implemented
- [ ] API versioning in place

#### Data Security
- [ ] Passwords are hashed (bcrypt, Argon2)
- [ ] Sensitive data not logged
- [ ] Connection strings in secure configuration
- [ ] Secrets not in source code

#### Dependencies
- [ ] Regular dependency updates
- [ ] No known vulnerabilities in packages
- [ ] Minimal dependency footprint

### 4. Common Vulnerabilities to Check

#### OWASP Top 10
1. Injection attacks
2. Broken authentication
3. Sensitive data exposure
4. XML external entities (XXE)
5. Broken access control
6. Security misconfiguration
7. Cross-site scripting (XSS)
8. Insecure deserialization
9. Using components with known vulnerabilities
10. Insufficient logging & monitoring

### 5. Security Scanning
```bash
# Check for vulnerable packages
dotnet list package --vulnerable

# Update packages
dotnet outdated
```

### 6. Code Review Focus
- No hardcoded credentials
- Proper error handling (no sensitive info in errors)
- Secure random number generation
- Proper session management
- Secure file upload handling

## PR Requirements
- No security vulnerabilities introduced
- Security checklist items addressed
- Sensitive operations properly protected

