using HealthDesk.Service;
using HealthDesk.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HealthDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService) => _patientService = patientService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _patientService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return patient is null ? NotFound() : Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientRequest request)
        {
            try
            {
                var patient = await _patientService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdatePatientRequest request)
        {
            var success = await _patientService.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _patientService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
