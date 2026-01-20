import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { PatientListComponent } from './patient-list.component';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient.model';

describe('PatientListComponent', () => {
  let component: PatientListComponent;
  let fixture: ComponentFixture<PatientListComponent>;
  let mockPatientService: jasmine.SpyObj<PatientService>;
  let mockRouter: jasmine.SpyObj<Router>;

  const mockPatients: Patient[] = [
    {
      id: '1',
      firstName: 'John',
      lastName: 'Doe',
      dateOfBirth: '1990-01-01',
      email: 'john@example.com',
      phoneNumber: '+1234567890',
      address: '123 Main St',
      createdAt: '2024-01-01T00:00:00Z'
    },
    {
      id: '2',
      firstName: 'Jane',
      lastName: 'Smith',
      dateOfBirth: '1985-05-15',
      email: 'jane@example.com',
      phoneNumber: '+0987654321',
      address: '456 Oak Ave',
      createdAt: '2024-01-02T00:00:00Z'
    }
  ];

  beforeEach(async () => {
    mockPatientService = jasmine.createSpyObj('PatientService', ['getAllPatients']);
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [PatientListComponent],
      providers: [
        { provide: PatientService, useValue: mockPatientService },
        { provide: Router, useValue: mockRouter }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PatientListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load patients on init', () => {
    // Arrange
    mockPatientService.getAllPatients.and.returnValue(of(mockPatients));

    // Act
    fixture.detectChanges(); // triggers ngOnInit

    // Assert
    expect(component.patients.length).toBe(2);
    expect(component.isLoading).toBe(false);
    expect(component.hasError).toBe(false);
  });

  it('should handle error when loading patients fails', () => {
    // Arrange
    const errorMessage = 'Failed to load';
    mockPatientService.getAllPatients.and.returnValue(
      throwError(() => new Error(errorMessage))
    );

    // Act
    fixture.detectChanges();

    // Assert
    expect(component.hasError).toBe(true);
    expect(component.errorMessage).toContain(errorMessage);
    expect(component.isLoading).toBe(false);
  });

  it('should show loading state while fetching patients', () => {
    // Arrange
    mockPatientService.getAllPatients.and.returnValue(of(mockPatients));

    // Act
    component.loadPatients();

    // Assert (before observable completes)
    expect(component.isLoading).toBe(true);
  });

  it('should navigate to new patient form when onAddPatient is called', () => {
    // Act
    component.onAddPatient();

    // Assert
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/patients/new']);
  });

  it('should format date correctly', () => {
    // Arrange
    const dateString = '1990-01-15';

    // Act
    const formatted = component.formatDate(dateString);

    // Assert
    expect(formatted).toContain('Jan');
    expect(formatted).toContain('15');
    expect(formatted).toContain('1990');
  });

  it('should return empty string for undefined date', () => {
    // Act
    const formatted = component.formatDate(undefined);

    // Assert
    expect(formatted).toBe('');
  });

  it('should calculate age correctly', () => {
    // Arrange
    const birthDate = new Date();
    birthDate.setFullYear(birthDate.getFullYear() - 30);
    const dateString = birthDate.toISOString().split('T')[0];

    // Act
    const age = component.getAge(dateString);

    // Assert
    expect(age).toBe(30);
  });

  it('should unsubscribe on destroy', () => {
    // Arrange
    spyOn(component['destroy$'], 'next');
    spyOn(component['destroy$'], 'complete');

    // Act
    component.ngOnDestroy();

    // Assert
    expect(component['destroy$'].next).toHaveBeenCalled();
    expect(component['destroy$'].complete).toHaveBeenCalled();
  });
});

