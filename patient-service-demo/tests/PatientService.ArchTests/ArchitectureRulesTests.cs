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

        // Act - Find all classes in Application layer
        var allApplicationClasses = assembly.GetTypes()
            .Where(t => t.IsClass &&
                       !t.IsAbstract &&
                       t.Namespace != null &&
                       t.Namespace.StartsWith(ApplicationNamespace))
            .ToList();

        var violations = new System.Collections.Generic.List<string>();
        var validSuffixes = new[] { "Command", "Query", "Validator", "Dto", "Response", "Request", "Handler", "Result", "Model" };

        foreach (var type in allApplicationClasses)
        {
            // Skip compiler-generated classes (async state machines, etc.)
            if (type.Name.Contains("<") || type.Name.Contains(">") ||
                type.GetCustomAttributes(false).Any(a => a.GetType().Name.Contains("CompilerGenerated")))
            {
                continue;
            }

            // Check if class is in a feature folder (e.g., CreatePatient, GetPatient, UpdatePatient)
            // Feature folders typically contain: Command, Handler, Validator
            var namespaceParts = type.Namespace?.Split('.') ?? Array.Empty<string>();
            var isInFeatureFolder = namespaceParts.Length > 3; // e.g., PatientService.Application.Patients.CreatePatient

            if (isInFeatureFolder)
            {
                // In feature folders, ALL classes must have a valid suffix
                var hasValidSuffix = validSuffixes.Any(suffix => type.Name.EndsWith(suffix));

                if (!hasValidSuffix)
                {
                    violations.Add(
                        $"❌ Class '{type.Name}' in namespace '{type.Namespace}' doesn't follow naming conventions.\n" +
                        $"   Classes in feature folders must end with: {string.Join(", ", validSuffixes)}\n" +
                        $"   Example: If this handles commands, rename to '{type.Name}Handler'");
                }
            }
            else
            {
                // Outside feature folders, check if class has handler-like methods
                var hasMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Any(m => m.Name.Contains("Handle") || m.Name.Contains("Execute"));

                // If it has handler-like methods but doesn't end with "Handler", it's a violation
                if (hasMethods && !type.Name.EndsWith("Handler"))
                {
                    violations.Add($"❌ Class '{type.Name}' has handler methods but doesn't end with 'Handler'");
                }
            }
        }

        // Assert
        Assert.True(violations.Count == 0,
            $"Found {violations.Count} naming violations:\n" + string.Join("\n", violations));
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

