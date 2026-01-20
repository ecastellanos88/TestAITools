using PatientService.Application.Patients.GetPatient;
using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.UnitTests.Patients;

public class GetPatientHandlerTests
{
    [Fact]
    public async Task HandleAsync_WithValidId_ReturnsPatient()
    {
        // Arrange
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com",
            PhoneNumber = "+1234567890",
            Address = "123 Main St"
        };

        var repository = new InMemoryPatientRepository();
        var addedPatient = await repository.AddAsync(patient);

        var handler = new GetPatientHandler(repository);
        var query = new GetPatientQuery(addedPatient.Id);

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addedPatient.Id, result.Id);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
        Assert.Equal("john.doe@example.com", result.Email);
    }

    [Fact]
    public async Task HandleAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        var handler = new GetPatientHandler(repository);
        var nonExistentId = Guid.NewGuid();
        var query = new GetPatientQuery(nonExistentId);

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task HandleAsync_WithEmptyRepository_ReturnsNull()
    {
        // Arrange
        var repository = new InMemoryPatientRepository();
        var handler = new GetPatientHandler(repository);
        var query = new GetPatientQuery(Guid.NewGuid());

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task HandleAsync_WithMultiplePatients_ReturnsCorrectPatient()
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

        var addedPatient1 = await repository.AddAsync(patient1);
        var addedPatient2 = await repository.AddAsync(patient2);

        var handler = new GetPatientHandler(repository);
        var query = new GetPatientQuery(addedPatient2.Id);

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addedPatient2.Id, result.Id);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("Smith", result.LastName);
        Assert.Equal("jane.smith@example.com", result.Email);
    }
}

