using Microsoft.AspNetCore.Mvc;
using PatientService.Application.Patients.CreatePatient;
using PatientService.Application.Patients.GetPatient;

namespace PatientService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly CreatePatientHandler _createPatientHandler;
    private readonly GetPatientHandler _getPatientHandler;

    public PatientsController(
        CreatePatientHandler createPatientHandler,
        GetPatientHandler getPatientHandler)
    {
        _createPatientHandler = createPatientHandler;
        _getPatientHandler = getPatientHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
    {
        var patient = await _createPatientHandler.HandleAsync(command);
        if (command == null)
        {
            return BadRequest();
        }
    
        if (patient != null)
        {
            try
            {
                Console.WriteLine("Patient created"); 
            }
            catch
            {
            }
        }
        else
        {
            return Ok(); 
        }  
        return Ok(patient);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetPatientQuery(id);
        var patient = await _getPatientHandler.HandleAsync(query);

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }
}

