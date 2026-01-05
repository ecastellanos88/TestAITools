using NetArchTest.Rules;
using Xunit;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

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

    [Fact]
    public void Controllers_Should_Be_Thin_And_Not_Contain_Business_Logic()
    {
        // Arrange
        var assembly = typeof(API.Controllers.PatientsController).Assembly;
        var controllerTypes = assembly.GetTypes()
            .Where(t => t.Namespace == "PatientService.API.Controllers" &&
                       t.Name.EndsWith("Controller"))
            .ToList();

        // Act & Assert
        foreach (var controllerType in controllerTypes)
        {
            var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                // Skip non-action methods
                if (!method.GetCustomAttributes(typeof(HttpPostAttribute), true).Any() &&
                    !method.GetCustomAttributes(typeof(HttpGetAttribute), true).Any() &&
                    !method.GetCustomAttributes(typeof(HttpPutAttribute), true).Any() &&
                    !method.GetCustomAttributes(typeof(HttpDeleteAttribute), true).Any())
                {
                    continue;
                }

                // Get the method body
                var methodBody = method.GetMethodBody();

                if (methodBody != null)
                {
                    // Check if method is too complex
                    // A thin controller method should have < 80 bytes of IL code
                    // Typical thin method: ~40-60 bytes (just call handler and return)
                    // Method with if statements: ~80+ bytes
                    var ilBytes = methodBody.GetILAsByteArray();
                    var ilByteCount = ilBytes?.Length ?? 0;

                    // Also check for branching instructions (if statements)
                    var hasBranching = ilBytes != null && ContainsBranchingLogic(ilBytes);

                    Assert.False(hasBranching,
                        $"Controller method '{controllerType.Name}.{method.Name}' contains conditional logic (if/else statements). " +
                        $"Controllers should be thin and only orchestrate calls to handlers. " +
                        $"Move validation and business logic to the Application layer (Handlers). " +
                        $"Controllers should NOT contain: if statements, loops, LINQ queries, or validation logic.");

                    Assert.True(ilByteCount < 80,
                        $"Controller method '{controllerType.Name}.{method.Name}' appears to contain business logic. " +
                        $"IL byte count: {ilByteCount}. " +
                        $"Controllers should be thin (< 80 IL bytes) and only orchestrate calls to handlers. " +
                        $"Move validation and business logic to the Application layer (Handlers).");
                }
            }
        }
    }

    private bool ContainsBranchingLogic(byte[] ilBytes)
    {
        // IL opcodes for branching (if statements, loops, etc.)
        // br.s = 0x2B, brfalse.s = 0x2C, brtrue.s = 0x2D
        // br = 0x38, brfalse = 0x39, brtrue = 0x3A
        // beq = 0x3B, bne.un = 0x40, bge = 0x3C, bgt = 0x3D, ble = 0x3E, blt = 0x3F

        for (int i = 0; i < ilBytes.Length; i++)
        {
            byte opcode = ilBytes[i];

            // Check for conditional branch instructions
            if (opcode == 0x2C || // brfalse.s
                opcode == 0x2D || // brtrue.s
                opcode == 0x39 || // brfalse
                opcode == 0x3A || // brtrue
                opcode == 0x3B || // beq
                opcode == 0x3C || // bge
                opcode == 0x3D || // bgt
                opcode == 0x3E || // ble
                opcode == 0x3F || // blt
                opcode == 0x40)   // bne.un
            {
                return true;
            }
        }

        return false;
    }
}

