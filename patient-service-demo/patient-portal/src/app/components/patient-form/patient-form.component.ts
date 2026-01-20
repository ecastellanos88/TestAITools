import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { PatientService } from '../../services/patient.service';
import { CreatePatientCommand } from '../../models/patient.model';

@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.css']
})
export class PatientFormComponent {
  patientForm: FormGroup;
  isSubmitting = false;
  submitSuccess = false;
  submitError = false;
  errorMessage = '';
  createdPatientId = '';

  constructor(
    private fb: FormBuilder,
    private patientService: PatientService
  ) {
    this.patientForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      dateOfBirth: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\+?[0-9]{10,15}$/)]],
      address: ['', [Validators.required, Validators.minLength(5)]]
    });
  }

  onSubmit(): void {
    if (this.patientForm.valid) {
      this.isSubmitting = true;
      this.submitSuccess = false;
      this.submitError = false;
      this.errorMessage = '';

      const command: CreatePatientCommand = {
        firstName: this.patientForm.value.firstName,
        lastName: this.patientForm.value.lastName,
        dateOfBirth: this.patientForm.value.dateOfBirth,
        email: this.patientForm.value.email,
        phoneNumber: this.patientForm.value.phoneNumber,
        address: this.patientForm.value.address
      };

      this.patientService.createPatient(command).subscribe({
        next: (patient) => {
          this.isSubmitting = false;
          this.submitSuccess = true;
          this.createdPatientId = patient.id || '';
          this.patientForm.reset();
          
          // Hide success message after 5 seconds
          setTimeout(() => {
            this.submitSuccess = false;
          }, 5000);
        },
        error: (error) => {
          this.isSubmitting = false;
          this.submitError = true;
          this.errorMessage = error.message || 'Failed to create patient';
          
          // Hide error message after 5 seconds
          setTimeout(() => {
            this.submitError = false;
          }, 5000);
        }
      });
    } else {
      // Mark all fields as touched to show validation errors
      Object.keys(this.patientForm.controls).forEach(key => {
        this.patientForm.get(key)?.markAsTouched();
      });
    }
  }

  getFieldError(fieldName: string): string {
    const field = this.patientForm.get(fieldName);
    
    if (field?.hasError('required')) {
      return 'This field is required';
    }
    if (field?.hasError('minlength')) {
      const minLength = field.errors?.['minlength'].requiredLength;
      return `Minimum length is ${minLength} characters`;
    }
    if (field?.hasError('email')) {
      return 'Please enter a valid email address';
    }
    if (field?.hasError('pattern')) {
      return 'Please enter a valid phone number';
    }
    
    return '';
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.patientForm.get(fieldName);
    return !!(field && field.invalid && field.touched);
  }

  resetForm(): void {
    this.patientForm.reset();
    this.submitSuccess = false;
    this.submitError = false;
    this.errorMessage = '';
  }
}

