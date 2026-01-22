using PatientService.Application.Patients.UpdatePatient;
using PatientService.Domain;
using PatientService.Infrastructure.Repositories;

namespace PatientService.UnitTests.Patients;

public class UpdatePatientHandlerTests
{
    private readonly IPatientRepository _repository;
    private readonly UpdatePatientHandler _handler;

    public UpdatePatientHandlerTests()
    {
        _repository = new InMemoryPatientRepository();
        _handler = new UpdatePatientHandler(_repository);
    }

    [Fact]
    public async Task HandleAsync_WithValidData_ReturnsUpdatedPatient()
    {
        // Arrange
        var originalPatient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St"
        };
        var createdPatient = await _repository.AddAsync(originalPatient);

        var command = new UpdatePatientCommand(
            createdPatient.Id,
            "Jane",
            "Smith",
            new DateTime(1992, 5, 15),
            "jane.smith@example.com",
            "0987654321",
            "456 Oak Ave"
        );

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(command.Id, result.Id);
        Assert.Equal("Jane", result.FirstName);
        Assert.Equal("Smith", result.LastName);
        Assert.Equal(new DateTime(1992, 5, 15), result.DateOfBirth);
        Assert.Equal("jane.smith@example.com", result.Email);
        Assert.Equal("0987654321", result.PhoneNumber);
        Assert.Equal("456 Oak Ave", result.Address);
    }

    [Fact]
    public async Task HandleAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var command = new UpdatePatientCommand(
            Guid.NewGuid(),
            "Jane",
            "Smith",
            new DateTime(1992, 5, 15),
            "jane.smith@example.com",
            "0987654321",
            "456 Oak Ave"
        );

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task HandleAsync_UpdatesAllFields_Correctly()
    {
        // Arrange
        var originalPatient = new Patient
        {
            FirstName = "Original",
            LastName = "Name",
            DateOfBirth = new DateTime(1980, 1, 1),
            Email = "original@example.com",
            PhoneNumber = "1111111111",
            Address = "Original Address"
        };
        var createdPatient = await _repository.AddAsync(originalPatient);

        var command = new UpdatePatientCommand(
            createdPatient.Id,
            "Updated",
            "Person",
            new DateTime(1985, 12, 31),
            "updated@example.com",
            "2222222222",
            "Updated Address"
        );

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated", result.FirstName);
        Assert.Equal("Person", result.LastName);
        Assert.Equal(new DateTime(1985, 12, 31), result.DateOfBirth);
        Assert.Equal("updated@example.com", result.Email);
        Assert.Equal("2222222222", result.PhoneNumber);
        Assert.Equal("Updated Address", result.Address);
    }

    [Fact]
    public async Task HandleAsync_SetsUpdatedAtTimestamp()
    {
        // Arrange
        var originalPatient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "john.doe@example.com",
            PhoneNumber = "1234567890",
            Address = "123 Main St"
        };
        var createdPatient = await _repository.AddAsync(originalPatient);

        var command = new UpdatePatientCommand(
            createdPatient.Id,
            "Jane",
            "Smith",
            new DateTime(1992, 5, 15),
            "jane.smith@example.com",
            "0987654321",
            "456 Oak Ave"
        );

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.UpdatedAt > createdPatient.CreatedAt);
    }
}

