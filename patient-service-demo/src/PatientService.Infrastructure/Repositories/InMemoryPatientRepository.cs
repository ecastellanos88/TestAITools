using PatientService.Domain;

namespace PatientService.Infrastructure.Repositories;

public interface IPatientRepository
{
    Task<Patient> AddAsync(Patient patient);
    Task<Patient?> GetByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> UpdateAsync(Patient patient);
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

    public Task<Patient?> GetByIdAsync(Guid id)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(patient);
    }

    public Task<IEnumerable<Patient>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Patient>>(_patients.ToList());
    }

    public Task<Patient?> UpdateAsync(Patient patient)
    {
        var existingPatient = _patients.FirstOrDefault(p => p.Id == patient.Id);
        if (existingPatient == null)
        {
            return Task.FromResult<Patient?>(null);
        }

        existingPatient.FirstName = patient.FirstName;
        existingPatient.LastName = patient.LastName;
        existingPatient.DateOfBirth = patient.DateOfBirth;
        existingPatient.Email = patient.Email;
        existingPatient.PhoneNumber = patient.PhoneNumber;
        existingPatient.Address = patient.Address;
        existingPatient.UpdatedAt = DateTime.UtcNow;

        return Task.FromResult<Patient?>(existingPatient);
    }
}

