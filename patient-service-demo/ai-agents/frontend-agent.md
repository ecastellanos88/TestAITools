# Frontend Agent - Angular Development Rules

## 🎯 Purpose
This agent enforces frontend development standards, best practices, and architectural patterns for the Angular-based Patient Management Portal.

---

## 📋 Technology Stack Requirements

### Angular Version
- **MUST use Angular 18.2.x** (current: 18.2.0)
- **MUST use Angular CLI 18.2.20**
- **MUST use TypeScript 5.5.x**
- **DO NOT upgrade** to newer versions without explicit approval
- **DO NOT downgrade** to older versions

### Core Dependencies
```json
{
  "@angular/core": "^18.2.0",
  "@angular/common": "^18.2.0",
  "@angular/forms": "^18.2.0",
  "@angular/router": "^18.2.0",
  "rxjs": "~7.8.0",
  "zone.js": "~0.14.10"
}
```

### Build Configuration
- **SSR (Server-Side Rendering)**: DISABLED
- **Prerendering**: DISABLED
- **Reason**: Simplifies deployment and avoids SSR-related errors

---

## 🎨 Design System & Styling

### Corporate Color Palette (MANDATORY)

All components MUST use these CSS variables defined in `styles.css`:

```css
:root {
  --primary-color: #1e3a8a;      /* Dark Blue - Primary actions */
  --primary-hover: #1e40af;      /* Blue - Hover states */
  --secondary-color: #6b7280;    /* Gray - Secondary text */
  --success-color: #059669;      /* Green - Success messages */
  --error-color: #dc2626;        /* Red - Errors */
  --background: #f9fafb;         /* Light Gray - Backgrounds */
  --surface: #ffffff;            /* White - Cards/Forms */
  --border: #e5e7eb;             /* Light Gray - Borders */
}
```

### Global Styles (DO NOT MODIFY)

**File**: `src/styles.css`

```css
/* CSS Reset */
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

/* Body Gradient Background */
body {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 
               'Helvetica Neue', Arial, sans-serif;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  min-height: 100vh;
  padding: 2rem;
  color: #1f2937;
}
```

### Component Styling Rules

1. **MUST use CSS files** (not SCSS/SASS/LESS)
2. **MUST follow BEM-like naming** for CSS classes
3. **MUST be responsive** (mobile-first approach)
4. **MUST use CSS Grid or Flexbox** for layouts
5. **MUST include hover states** for interactive elements
6. **MUST include focus states** for accessibility
7. **MUST use transitions** for smooth UX (0.2s default)

### Standard Component CSS Pattern

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

/* Form Controls */
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

/* Buttons */
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
```

### Responsive Breakpoints

```css
/* Mobile First */
@media (max-width: 768px) {
  .form-row {
    grid-template-columns: 1fr;
  }
}

@media (min-width: 769px) and (max-width: 1024px) {
  /* Tablet styles */
}

@media (min-width: 1025px) {
  /* Desktop styles */
}
```

---

## 🏗️ Architecture Patterns

### Component Structure (MANDATORY)

```
src/app/
├── components/          # Feature components
│   ├── patient-form/
│   │   ├── patient-form.component.ts
│   │   ├── patient-form.component.html
│   │   ├── patient-form.component.css
│   │   └── patient-form.component.spec.ts
├── models/             # TypeScript interfaces
│   └── patient.model.ts
├── services/           # HTTP services
│   └── patient.service.ts
├── environments/       # Environment configs
│   ├── environment.ts
│   └── environment.prod.ts
└── app.component.ts    # Root component
```

### Standalone Components (REQUIRED)

**MUST use standalone components** (Angular 18 best practice):

```typescript
@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './patient-form.component.html',
  styleUrl: './patient-form.component.css'
})
export class PatientFormComponent {
  // Component logic
}
```

---

## 📝 Forms & Validation

### Reactive Forms (MANDATORY)

**MUST use Reactive Forms** (NOT Template-driven forms):

```typescript
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export class PatientFormComponent {
  patientForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.patientForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[0-9]{10,15}$/)]],
      // ... more fields
    });
  }
}
```

### Validation Rules

**Standard Validators:**
- `Validators.required` - For mandatory fields
- `Validators.minLength(n)` - Minimum character length
- `Validators.maxLength(n)` - Maximum character length
- `Validators.email` - Email format validation
- `Validators.pattern(regex)` - Custom regex patterns

**Custom Validators:**
- Create in separate `validators/` folder
- Must be reusable across components
- Must return `ValidationErrors | null`

### Error Messages (MUST IMPLEMENT)

```html
<div class="form-group">
  <label class="form-label">
    First Name <span class="required">*</span>
  </label>
  <input
    type="text"
    formControlName="firstName"
    class="form-control"
    [class.invalid]="firstName?.invalid && firstName?.touched"
  />
  <div class="error-message" *ngIf="firstName?.invalid && firstName?.touched">
    <span *ngIf="firstName?.errors?.['required']">First name is required</span>
    <span *ngIf="firstName?.errors?.['minlength']">
      Minimum 2 characters required
    </span>
  </div>
</div>
```

### Form Submission Pattern

```typescript
onSubmit(): void {
  if (this.patientForm.invalid) {
    this.patientForm.markAllAsTouched();
    return;
  }

  this.isLoading = true;
  this.patientService.createPatient(this.patientForm.value)
    .subscribe({
      next: (response) => {
        this.showSuccess('Patient created successfully!');
        this.patientForm.reset();
        this.isLoading = false;
      },
      error: (error) => {
        this.showError('Failed to create patient. Please try again.');
        this.isLoading = false;
      }
    });
}
```

---

## 🔌 Services & HTTP

### Service Structure (MANDATORY)

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = `${environment.apiUrl}/patients`;

  constructor(private http: HttpClient) {}

  createPatient(command: CreatePatientCommand): Observable<Patient> {
    return this.http.post<Patient>(this.apiUrl, command)
      .pipe(catchError(this.handleError));
  }

  getPatientById(id: string): Observable<Patient> {
    return this.http.get<Patient>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    console.error('API Error:', error);
    return throwError(() => new Error('An error occurred. Please try again.'));
  }
}
```

### HTTP Rules

1. **MUST use environment variables** for API URLs
2. **MUST implement error handling** with `catchError`
3. **MUST use TypeScript interfaces** for request/response types
4. **MUST use RxJS operators** for data transformation
5. **MUST unsubscribe** from observables (use `async` pipe or `takeUntil`)
6. **MUST NOT hardcode URLs** in components

### Environment Configuration

**File**: `src/environments/environment.ts`

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

**File**: `src/environments/environment.prod.ts`

```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.yourcompany.com/api'
};
```

---

## 🎭 User Experience Patterns

### Loading States (REQUIRED)

```typescript
export class PatientFormComponent {
  isLoading = false;

  onSubmit(): void {
    this.isLoading = true;
    // ... API call
  }
}
```

```html
<button
  type="submit"
  class="btn btn-primary"
  [disabled]="patientForm.invalid || isLoading"
>
  {{ isLoading ? 'Creating...' : 'Create Patient' }}
</button>
```

### Success/Error Messages (REQUIRED)

```typescript
export class PatientFormComponent {
  successMessage: string | null = null;
  errorMessage: string | null = null;

  showSuccess(message: string): void {
    this.successMessage = message;
    this.errorMessage = null;
    setTimeout(() => this.successMessage = null, 5000);
  }

  showError(message: string): void {
    this.errorMessage = message;
    this.successMessage = null;
    setTimeout(() => this.errorMessage = null, 5000);
  }
}
```

```html
<div class="alert alert-success" *ngIf="successMessage">
  <span class="alert-icon">✓</span>
  <div class="alert-content">{{ successMessage }}</div>
</div>

<div class="alert alert-error" *ngIf="errorMessage">
  <span class="alert-icon">✕</span>
  <div class="alert-content">{{ errorMessage }}</div>
</div>
```

### Animations (RECOMMENDED)

```css
@keyframes slideIn {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.alert {
  animation: slideIn 0.3s ease-out;
}
```

---

## 🧪 Testing Standards

### Unit Testing (REQUIRED)

**MUST write unit tests** for all components and services:

```typescript
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { PatientFormComponent } from './patient-form.component';
import { PatientService } from '../../services/patient.service';
import { of, throwError } from 'rxjs';

describe('PatientFormComponent', () => {
  let component: PatientFormComponent;
  let fixture: ComponentFixture<PatientFormComponent>;
  let mockPatientService: jasmine.SpyObj<PatientService>;

  beforeEach(async () => {
    mockPatientService = jasmine.createSpyObj('PatientService', ['createPatient']);

    await TestBed.configureTestingModule({
      imports: [PatientFormComponent, ReactiveFormsModule],
      providers: [
        { provide: PatientService, useValue: mockPatientService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PatientFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should invalidate form when fields are empty', () => {
    expect(component.patientForm.valid).toBeFalsy();
  });

  it('should validate form when all fields are filled correctly', () => {
    component.patientForm.patchValue({
      firstName: 'John',
      lastName: 'Doe',
      dateOfBirth: '1990-01-01',
      email: 'john@example.com',
      phoneNumber: '+1234567890',
      address: '123 Main St'
    });
    expect(component.patientForm.valid).toBeTruthy();
  });
});
```

### Test Coverage Requirements

- **Minimum 80% code coverage**
- **MUST test all public methods**
- **MUST test form validation**
- **MUST test error handling**
- **MUST test success scenarios**

---

## 📦 Models & Interfaces

### TypeScript Interfaces (MANDATORY)

**File**: `src/app/models/patient.model.ts`

```typescript
export interface Patient {
  id?: string;
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  email: string;
  phoneNumber: string;
  address: string;
  createdAt?: string;
  updatedAt?: string;
}

export interface CreatePatientCommand {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  email: string;
  phoneNumber: string;
  address: string;
}

export interface UpdatePatientCommand extends CreatePatientCommand {
  id: string;
}
```

### Interface Rules

1. **MUST use interfaces** (not classes) for data models
2. **MUST match backend DTOs** exactly
3. **MUST use optional properties** (`?`) for nullable fields
4. **MUST use string for dates** (ISO 8601 format)
5. **MUST export all interfaces**
6. **MUST group related interfaces** in same file

---

## 🔒 Security Best Practices

### Input Sanitization

1. **MUST validate all user inputs** on the client side
2. **MUST NOT trust client-side validation alone** (backend validates too)
3. **MUST sanitize HTML** if displaying user-generated content
4. **MUST use Angular's built-in XSS protection**

### CORS Configuration

**Backend must allow frontend origin:**

```csharp
// Backend: Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
```

### Environment Variables

**NEVER commit sensitive data:**
- API keys
- Passwords
- Production URLs

**Use environment files:**
- `environment.ts` - Development (committed)
- `environment.prod.ts` - Production template (committed)
- `environment.local.ts` - Local overrides (gitignored)

---

## ♿ Accessibility (A11y)

### WCAG 2.1 AA Compliance (REQUIRED)

1. **MUST use semantic HTML**
   ```html
   <form> <!-- Not <div> -->
   <button> <!-- Not <div onclick> -->
   <label> <!-- For all inputs -->
   ```

2. **MUST provide labels** for all form controls
   ```html
   <label for="firstName">First Name</label>
   <input id="firstName" type="text" />
   ```

3. **MUST use ARIA attributes** when needed
   ```html
   <div role="alert" aria-live="polite">
     {{ errorMessage }}
   </div>
   ```

4. **MUST ensure keyboard navigation**
   - All interactive elements must be focusable
   - Tab order must be logical
   - Focus states must be visible

5. **MUST provide sufficient color contrast**
   - Text: 4.5:1 minimum
   - Large text: 3:1 minimum
   - Use tools like WebAIM Contrast Checker

---

## 🚀 Performance Optimization

### Bundle Size

**Current budgets** (defined in `angular.json`):

```json
{
  "budgets": [
    {
      "type": "initial",
      "maximumWarning": "500kB",
      "maximumError": "1MB"
    },
    {
      "type": "anyComponentStyle",
      "maximumWarning": "2kB",
      "maximumError": "4kB"
    }
  ]
}
```

### Optimization Rules

1. **MUST use lazy loading** for routes
   ```typescript
   const routes: Routes = [
     {
       path: 'patients',
       loadComponent: () => import('./components/patient-list/patient-list.component')
         .then(m => m.PatientListComponent)
     }
   ];
   ```

2. **MUST use OnPush change detection** for performance-critical components
   ```typescript
   @Component({
     changeDetection: ChangeDetectionStrategy.OnPush
   })
   ```

3. **MUST use trackBy** for *ngFor loops
   ```html
   <div *ngFor="let patient of patients; trackBy: trackByPatientId">
   ```

4. **MUST unsubscribe** from observables
   ```typescript
   // Use async pipe (preferred)
   patients$ = this.patientService.getPatients();

   // Or use takeUntil
   private destroy$ = new Subject<void>();

   ngOnDestroy(): void {
     this.destroy$.next();
     this.destroy$.complete();
   }
   ```

---

## 📱 Responsive Design

### Mobile-First Approach (MANDATORY)

```css
/* Base styles for mobile */
.container {
  padding: 1rem;
}

/* Tablet and up */
@media (min-width: 768px) {
  .container {
    padding: 2rem;
  }
}

/* Desktop and up */
@media (min-width: 1024px) {
  .container {
    padding: 3rem;
  }
}
```

### Touch-Friendly Design

1. **Minimum touch target size**: 44x44 pixels
2. **Adequate spacing** between interactive elements
3. **No hover-only interactions** (must work on touch devices)

---

## 📂 File Naming Conventions

### Component Files

```
patient-form.component.ts       # Component logic
patient-form.component.html     # Template
patient-form.component.css      # Styles
patient-form.component.spec.ts  # Unit tests
```

### Service Files

```
patient.service.ts              # Service logic
patient.service.spec.ts         # Unit tests
```

### Model Files

```
patient.model.ts                # Interfaces
patient.types.ts                # Type definitions
patient.constants.ts            # Constants
```

### Naming Rules

1. **MUST use kebab-case** for file names
2. **MUST use PascalCase** for class names
3. **MUST use camelCase** for variables and methods
4. **MUST use UPPER_SNAKE_CASE** for constants
5. **MUST use descriptive names** (no abbreviations)

---

## 🔄 State Management

### Component State (SIMPLE APPS)

For simple applications, use component-level state:

```typescript
export class PatientFormComponent {
  // State
  patients: Patient[] = [];
  isLoading = false;
  errorMessage: string | null = null;

  // Methods to update state
  loadPatients(): void {
    this.isLoading = true;
    this.patientService.getPatients().subscribe({
      next: (data) => {
        this.patients = data;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load patients';
        this.isLoading = false;
      }
    });
  }
}
```

### NgRx (COMPLEX APPS)

**Only use NgRx if:**
- Application has complex state
- Multiple components share state
- Need time-travel debugging
- Explicit approval from tech lead

---

## 🛠️ Development Workflow

### Before Starting Development

1. **Pull latest changes** from repository
2. **Install dependencies**: `npm install`
3. **Verify Angular version**: `ng version`
4. **Run tests**: `npm test`
5. **Start dev server**: `ng serve`

### During Development

1. **Follow this agent's rules** strictly
2. **Write tests** alongside code
3. **Test in browser** frequently
4. **Check console** for errors/warnings
5. **Validate forms** thoroughly

### Before Committing

1. **Run linter**: `ng lint` (if configured)
2. **Run tests**: `npm test`
3. **Build production**: `ng build --configuration production`
4. **Check bundle size** warnings
5. **Test in multiple browsers**

---

## 🚫 Common Mistakes to Avoid

### ❌ DON'T

1. **DON'T use `any` type**
   ```typescript
   // ❌ Bad
   let data: any;

   // ✅ Good
   let data: Patient;
   ```

2. **DON'T subscribe in templates**
   ```html
   <!-- ❌ Bad -->
   <div>{{ patients.subscribe() }}</div>

   <!-- ✅ Good -->
   <div *ngFor="let patient of patients$ | async">
   ```

3. **DON'T forget to unsubscribe**
   ```typescript
   // ❌ Bad
   ngOnInit() {
     this.service.getData().subscribe();
   }

   // ✅ Good
   ngOnInit() {
     this.service.getData()
       .pipe(takeUntil(this.destroy$))
       .subscribe();
   }
   ```

4. **DON'T mutate state directly**
   ```typescript
   // ❌ Bad
   this.patients.push(newPatient);

   // ✅ Good
   this.patients = [...this.patients, newPatient];
   ```

5. **DON'T hardcode values**
   ```typescript
   // ❌ Bad
   const apiUrl = 'http://localhost:5000/api';

   // ✅ Good
   const apiUrl = environment.apiUrl;
   ```

---

## 📚 Code Review Checklist

### Before Submitting PR

- [ ] All tests pass (`npm test`)
- [ ] No console errors or warnings
- [ ] Code follows style guide
- [ ] Components use standalone pattern
- [ ] Forms use reactive forms
- [ ] Services use proper error handling
- [ ] CSS uses corporate color palette
- [ ] Responsive design implemented
- [ ] Accessibility requirements met
- [ ] No hardcoded values
- [ ] TypeScript strict mode passes
- [ ] Bundle size within limits
- [ ] Documentation updated (if needed)

---

## 🎓 Learning Resources

### Official Documentation

- [Angular Documentation](https://angular.dev)
- [Angular Style Guide](https://angular.dev/style-guide)
- [RxJS Documentation](https://rxjs.dev)
- [TypeScript Handbook](https://www.typescriptlang.org/docs/)

### Best Practices

- [Angular Best Practices](https://angular.dev/best-practices)
- [Web Accessibility (WCAG)](https://www.w3.org/WAI/WCAG21/quickref/)
- [CSS Guidelines](https://cssguidelin.es/)

---

## 🔧 Troubleshooting

### Common Issues

#### Port Already in Use

```powershell
# Find process using port 4200
netstat -ano | findstr :4200

# Kill process (replace PID)
taskkill /PID <PID> /F
```

#### SSR Errors

If you see `document is not defined`:
1. Check `angular.json`
2. Ensure SSR is disabled
3. Remove `server` and `prerender` options

#### CORS Errors

1. Verify backend is running on `http://localhost:5000`
2. Check CORS policy in backend `Program.cs`
3. Ensure frontend uses correct API URL in `environment.ts`

#### Build Errors

```bash
# Clear cache
rm -rf .angular/cache
rm -rf node_modules
npm install

# Rebuild
ng build
```

---

## ✅ Summary

This agent ensures:

1. **Consistent Angular 18.2.x usage**
2. **Corporate design system compliance**
3. **Reactive forms with validation**
4. **Proper service architecture**
5. **Accessibility standards**
6. **Performance optimization**
7. **Security best practices**
8. **Comprehensive testing**

**Remember**: These rules exist to maintain code quality, consistency, and user experience. Follow them strictly unless you have explicit approval to deviate.

---

## 📞 Support

For questions or clarifications:
1. Review this document first
2. Check official Angular documentation
3. Consult with tech lead
4. Create a discussion in team chat

**Last Updated**: 2024-01-20
**Version**: 1.0.0
**Maintained By**: Development Team

