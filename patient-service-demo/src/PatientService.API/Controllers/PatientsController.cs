using Microsoft.AspNetCore.Mvc;
using PatientService.Application.Patients.CreatePatient;
using PatientService.Application.Patients.GetPatient;
using PatientService.Application.Patients.GetAllPatients;
using PatientService.Application.Patients.UpdatePatient;

namespace PatientService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly CreatePatientHandler _createPatientHandler;
    private readonly GetPatientHandler _getPatientHandler;
    private readonly GetAllPatientsHandler _getAllPatientsHandler;
    private readonly UpdatePatientHandler _updatePatientHandler;

    public PatientsController(
        CreatePatientHandler createPatientHandler,
        GetPatientHandler getPatientHandler,
        GetAllPatientsHandler getAllPatientsHandler,
        UpdatePatientHandler updatePatientHandler)
    {
        _createPatientHandler = createPatientHandler;
        _getPatientHandler = getPatientHandler;
        _getAllPatientsHandler = getAllPatientsHandler;
        _updatePatientHandler = updatePatientHandler;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPatientsQuery();
        var patients = await _getAllPatientsHandler.HandleAsync(query);
        return Ok(patients);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePatientCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch");
        }

        var patient = await _updatePatientHandler.HandleAsync(command);

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }
}

