using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.Application.Patients.GetAllPatients;

public class GetAllPatientsHandler
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientsHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<Patient>> HandleAsync(GetAllPatientsQuery query)
    {
        return await _patientRepository.GetAllAsync();
    }
}

