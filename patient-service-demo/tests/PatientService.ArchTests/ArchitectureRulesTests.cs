using NetArchTest.Rules;
using Xunit;

namespace PatientService.ArchTests;

public class ArchitectureRulesTests
{
    private const string DomainNamespace = "PatientService.Domain";
    private const string ApplicationNamespace = "PatientService.Application";
    private const string InfrastructureNamespace = "PatientService.Infrastructure";
    private const string ApiNamespace = "PatientService.API";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        var assembly = typeof(Domain.Patient).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace(DomainNamespace)
            .ShouldNot()
            .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace, ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOn_API()
    {
        // Arrange
        var assembly = typeof(Application.Patients.CreatePatient.CreatePatientCommand).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace(ApplicationNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOn_API()
    {
        // Arrange
        var assembly = typeof(Infrastructure.Repositories.InMemoryPatientRepository).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace(InfrastructureNamespace)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Controllers_Should_HaveName_EndingWith_Controller()
    {
        // Arrange
        var assembly = typeof(API.Controllers.PatientsController).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .ResideInNamespace("PatientService.API.Controllers")
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Handlers_Should_HaveName_EndingWith_Handler()
    {
        // Arrange
        var assembly = typeof(Application.Patients.CreatePatient.CreatePatientHandler).Assembly;

        // Act
        var result = Types.InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .ResideInNamespace(ApplicationNamespace)
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful);
    }
}

