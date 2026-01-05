namespace PatientService.Application.Patients.CreatePatient;

public record CreatePatientCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string Email,
    string PhoneNumber,
    string Address
);

