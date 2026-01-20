# Frontend Development Guidelines - Quick Reference

## 🎯 Quick Start

This document provides quick reference for frontend development rules. For complete details, see `/ai-agents/frontend-agent.md`.

---

## 📋 Technology Stack

- **Angular**: 18.2.0 (DO NOT CHANGE)
- **Angular CLI**: 18.2.20
- **TypeScript**: 5.5.x
- **RxJS**: 7.8.0
- **SSR**: DISABLED

---

## 🎨 Corporate Colors (MANDATORY)

```css
--primary-color: #1e3a8a;      /* Dark Blue */
--primary-hover: #1e40af;      /* Blue Hover */
--secondary-color: #6b7280;    /* Gray */
--success-color: #059669;      /* Green */
--error-color: #dc2626;        /* Red */
--background: #f9fafb;         /* Light Gray */
--surface: #ffffff;            /* White */
--border: #e5e7eb;             /* Border Gray */
```

**Usage:**
```css
.btn-primary {
  background-color: var(--primary-color);
}
```

---

## 🏗️ Project Structure

```
src/app/
├── components/          # Feature components
│   └── patient-form/
├── models/             # TypeScript interfaces
│   └── patient.model.ts
├── services/           # HTTP services
│   └── patient.service.ts
├── environments/       # Environment configs
│   ├── environment.ts
│   └── environment.prod.ts
└── app.component.ts    # Root component
```

---

## 📝 Component Template

```typescript
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-my-component',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './my-component.component.html',
  styleUrl: './my-component.component.css'
})
export class MyComponentComponent {
  myForm: FormGroup;
  isLoading = false;
  successMessage: string | null = null;
  errorMessage: string | null = null;

  constructor(private fb: FormBuilder) {
    this.myForm = this.fb.group({
      field: ['', [Validators.required]]
    });
  }

  onSubmit(): void {
    if (this.myForm.invalid) {
      this.myForm.markAllAsTouched();
      return;
    }
    // Handle submission
  }
}
```

---

## 🔌 Service Template

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MyService {
  private apiUrl = `${environment.apiUrl}/resource`;

  constructor(private http: HttpClient) {}

  getData(): Observable<any> {
    return this.http.get<any>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('API Error:', error);
    return throwError(() => new Error('An error occurred'));
  }
}
```

---

## 🎨 CSS Template

```css
/* Container */
.component-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 2rem;
  background: var(--surface);
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

/* Form Control */
.form-control {
  padding: 0.75rem;
  border: 1px solid var(--border);
  border-radius: 6px;
  font-size: 1rem;
  transition: all 0.2s;
}

.form-control:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

/* Button */
.btn-primary {
  background-color: var(--primary-color);
  color: white;
  padding: 0.75rem 2rem;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: all 0.2s;
}

.btn-primary:hover:not(:disabled) {
  background-color: var(--primary-hover);
  transform: translateY(-1px);
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

/* Responsive */
@media (max-width: 768px) {
  .component-container {
    padding: 1rem;
  }
}
```

---

## ✅ Checklist Before Committing

- [ ] `npm test` passes
- [ ] No console errors
- [ ] Responsive design works
- [ ] Forms validated
- [ ] Error handling implemented
- [ ] Loading states added
- [ ] CSS uses corporate colors
- [ ] TypeScript types defined
- [ ] No `any` types used
- [ ] Accessibility checked

---

## 🚀 Common Commands

```bash
# Start development server
ng serve

# Run tests
npm test

# Build for production
ng build --configuration production

# Check Angular version
ng version

# Generate component
ng generate component components/my-component --standalone

# Generate service
ng generate service services/my-service
```

---

## 🐛 Quick Troubleshooting

### Port 4200 in use
```powershell
netstat -ano | findstr :4200
taskkill /PID <PID> /F
```

### Clear cache
```bash
rm -rf .angular/cache
rm -rf node_modules
npm install
```

### CORS error
- Check backend is running on `http://localhost:5000`
- Verify `environment.ts` has correct API URL
- Check backend CORS policy

---

## 📚 Key Rules

1. ✅ **ALWAYS** use Angular 18.2.x
2. ✅ **ALWAYS** use Reactive Forms
3. ✅ **ALWAYS** use standalone components
4. ✅ **ALWAYS** use corporate color palette
5. ✅ **ALWAYS** implement error handling
6. ✅ **ALWAYS** add loading states
7. ✅ **ALWAYS** validate forms
8. ✅ **ALWAYS** write tests
9. ❌ **NEVER** use `any` type
10. ❌ **NEVER** hardcode URLs

---

For complete guidelines, see: `../ai-agents/frontend-agent.md`

