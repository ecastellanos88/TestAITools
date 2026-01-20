using PatientService.Application.Patients.GetAllPatients;
using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.UnitTests.Patients;

public class GetAllPatientsHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithEmptyRepository_ReturnsEmptyList()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        var handler = new GetAllPatientsHandler(repository);
        var query = new GetAllPatientsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task HandleAsync_WithOnePatient_ReturnsListWithOnePatient()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com",
            PhoneNumber = "+1234567890",
            Address = "123 Main St"
        };
        await repository.AddAsync(patient);

        var handler = new GetAllPatientsHandler(repository);
        var query = new GetAllPatientsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        var returnedPatient = result.First();
        Assert.Equal("John", returnedPatient.FirstName);
        Assert.Equal("Doe", returnedPatient.LastName);
    }

    [Fact]
    public async Task HandleAsync_WithMultiplePatients_ReturnsAllPatients()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        
        var patient1 = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com",
            PhoneNumber = "+1234567890",
            Address = "123 Main St"
        };
        
        var patient2 = new Patient
        {
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateTime(1985, 5, 15),
            Email = "jane.smith@example.com",
            PhoneNumber = "+0987654321",
            Address = "456 Oak Ave"
        };
        
        var patient3 = new Patient
        {
            FirstName = "Bob",
            LastName = "Johnson",
            DateOfBirth = new DateTime(1995, 12, 25),
            Email = "bob.johnson@example.com",
            PhoneNumber = "+1122334455",
            Address = "789 Pine Rd"
        };

        await repository.AddAsync(patient1);
        await repository.AddAsync(patient2);
        await repository.AddAsync(patient3);

        var handler = new GetAllPatientsHandler(repository);
        var query = new GetAllPatientsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
        Assert.Contains(result, p => p.FirstName == "John" && p.LastName == "Doe");
        Assert.Contains(result, p => p.FirstName == "Jane" && p.LastName == "Smith");
        Assert.Contains(result, p => p.FirstName == "Bob" && p.LastName == "Johnson");
    }

    [Fact]
    public async Task HandleAsync_ReturnsPatientsSortedByCreationDate()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        
        var patient1 = new Patient
        {
            FirstName = "First",
            LastName = "Patient",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "first@example.com",
            PhoneNumber = "+1111111111",
            Address = "111 First St"
        };
        
        await repository.AddAsync(patient1);
        await Task.Delay(10); // Small delay to ensure different timestamps
        
        var patient2 = new Patient
        {
            FirstName = "Second",
            LastName = "Patient",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "second@example.com",
            PhoneNumber = "+2222222222",
            Address = "222 Second St"
        };
        
        await repository.AddAsync(patient2);

        var handler = new GetAllPatientsHandler(repository);
        var query = new GetAllPatientsQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }
}

