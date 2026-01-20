# Patient Portal - Angular Frontend

## 📋 Overview

Modern, responsive Angular 18 application for patient management with corporate styling and best practices.

**Generated with**: [Angular CLI](https://github.com/angular/angular-cli) version 18.2.20

---

## 🚀 Quick Start

### Prerequisites

- Node.js 20.x or higher
- npm 10.x or higher
- Angular CLI 18.2.20

### Installation & Running

```bash
# Install dependencies
npm install

# Start development server
ng serve

# Open browser at http://localhost:4200
```

---

## 📁 Project Structure

```
src/
├── app/
│   ├── components/          # Feature components
│   │   └── patient-form/    # Patient creation form
│   ├── models/              # TypeScript interfaces
│   │   └── patient.model.ts
│   ├── services/            # HTTP services
│   │   └── patient.service.ts
│   ├── environments/        # Environment configs
│   │   ├── environment.ts
│   │   └── environment.prod.ts
│   ├── app.component.ts     # Root component
│   ├── app.config.ts        # App configuration
│   └── app.routes.ts        # Routing configuration
├── styles.css               # Global styles
└── index.html               # Main HTML file
```

---

## 🎨 Design System

### Corporate Colors

```css
Primary:    #1e3a8a (Dark Blue)
Secondary:  #6b7280 (Gray)
Success:    #059669 (Green)
Error:      #dc2626 (Red)
Background: #f9fafb (Light Gray)
Surface:    #ffffff (White)
Border:     #e5e7eb (Light Gray)
```

---

## 🛠️ Development Commands

```bash
# Development server
ng serve

# Generate component (standalone)
ng generate component components/my-component --standalone

# Generate service
ng generate service services/my-service

# Generate interface
ng generate interface models/my-model

# Build for production
ng build --configuration production

# Run tests
ng test

# Watch mode (auto-rebuild)
ng build --watch --configuration development
```

---

## 🧪 Testing

```bash
# Run all tests
ng test

# Run tests with coverage
ng test --code-coverage

# Run tests in headless mode
ng test --browsers=ChromeHeadless --watch=false
```

**Test Coverage Target**: Minimum 80%

---

## 📦 Building

### Development Build

```bash
ng build
```

### Production Build

```bash
ng build --configuration production
```

**Output**: `dist/patient-portal/`

**Bundle Size Limits**:
- Initial: 500kB (warning), 1MB (error)
- Component Styles: 2kB (warning), 4kB (error)

---

## 🔧 Configuration

### Environment Variables

**Development** (`src/environments/environment.ts`):
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

**Production** (`src/environments/environment.prod.ts`):
```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.yourcompany.com/api'
};
```

---

## 📚 Documentation

- **Quick Reference**: `FRONTEND-GUIDELINES.md`
- **Complete Rules**: `../ai-agents/frontend-agent.md`
- **Execution Guide**: `../COMO-EJECUTAR.md`
- **Manual Instructions**: `../EJECUTAR-MANUAL.md`

---

## 🐛 Troubleshooting

### Port 4200 Already in Use

```powershell
# Find process
netstat -ano | findstr :4200

# Kill process (replace <PID>)
taskkill /PID <PID> /F
```

### Clear Cache & Reinstall

```bash
rm -rf .angular/cache
rm -rf node_modules
npm install
```

### CORS Errors

1. Ensure backend is running on `http://localhost:5000`
2. Check `environment.ts` has correct API URL
3. Verify backend CORS policy allows `http://localhost:4200`

### SSR Errors

If you see `document is not defined`:
- SSR is already disabled in `angular.json`
- If error persists, check for `server` or `prerender` options

---

## ✅ Code Quality Standards

### Must Follow

- ✅ Angular 18.2.x (DO NOT CHANGE)
- ✅ Standalone components
- ✅ Reactive forms
- ✅ Corporate color palette
- ✅ TypeScript strict mode
- ✅ Error handling
- ✅ Loading states
- ✅ Form validation
- ✅ Responsive design
- ✅ Accessibility (WCAG 2.1 AA)

### Linting

```bash
# Run linter (if configured)
ng lint
```

---

## 🚀 Deployment

1. Build for production:
   ```bash
   ng build --configuration production
   ```

2. Copy `dist/patient-portal/` to web server

3. Configure server to serve `index.html` for all routes

4. Update `environment.prod.ts` with production API URL

---

## 🔒 Security

- XSS protection via Angular sanitization
- CORS configured on backend
- No sensitive data in frontend code
- Environment variables for configuration
- Input validation on all forms

---

## 📊 Performance

- Lazy loading enabled
- Production build minified
- Tree shaking enabled
- AOT compilation enabled
- OnPush change detection (where applicable)

---

## 📞 Support & Resources

### Documentation
- [Angular Documentation](https://angular.dev)
- [Angular CLI Reference](https://angular.dev/tools/cli)
- [RxJS Documentation](https://rxjs.dev)

### Project Help
1. Check `FRONTEND-GUIDELINES.md`
2. Review `/ai-agents/frontend-agent.md`
3. Consult Angular documentation
4. Contact development team

---

## 📝 License

Copyright © 2024 Patient Management Portal. All rights reserved.
