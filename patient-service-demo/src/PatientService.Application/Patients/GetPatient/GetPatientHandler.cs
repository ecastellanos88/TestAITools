using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.Application.Patients.GetPatient;

public class GetPatientHandler
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Patient?> HandleAsync(GetPatientQuery query)
    {
        return await _patientRepository.GetByIdAsync(query.Id);
    }
}

