using PatientService.Domain;

namespace PatientService.Infrastructure.Repositories;

public interface IPatientRepository
{
    Task<Patient> AddAsync(Patient patient);
}

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients = new();

    public Task<Patient> AddAsync(Patient patient)
    {
        patient.Id = Guid.NewGuid();
        patient.CreatedAt = DateTime.UtcNow;
        _patients.Add(patient);
        return Task.FromResult(patient);
    }
}

