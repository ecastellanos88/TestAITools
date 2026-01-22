using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.Application.Patients.UpdatePatient;

public class UpdatePatientHandler
{
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Patient?> HandleAsync(UpdatePatientCommand command)
    {
        var patient = new Patient
        {
            Id = command.Id,
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
            Email = command.Email,
            PhoneNumber = command.PhoneNumber,
            Address = command.Address
        };

        return await _patientRepository.UpdateAsync(patient);
    }
}

