using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.Application.Patients.CreatePatient;

public class CreatePatientHandler
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Patient> HandleAsync(CreatePatientCommand command)
    {
        var patient = new Patient
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Address = command.Address
        };

        return await _patientRepository.AddAsync(patient);
    }
}

