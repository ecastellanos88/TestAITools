using Microsoft.AspNetCore.Mvc;
using PatientService.Application.Patients.CreatePatient;

namespace PatientService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly CreatePatientHandler _createPatientHandler;

    public PatientsController(CreatePatientHandler createPatientHandler)
    {
        _createPatientHandler = createPatientHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
    {
        var patient = await _createPatientHandler.HandleAsync(command);
        return Ok(patient);
    }
}

