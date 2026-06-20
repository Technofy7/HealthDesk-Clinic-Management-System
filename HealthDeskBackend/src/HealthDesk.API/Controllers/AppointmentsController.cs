using HealthDesk.Service;
using HealthDesk.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HealthDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService) => _appointmentService = appointmentService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _appointmentService.GetAllAsync());

        [HttpGet("patient/{patientId:int}")]
        public async Task<IActionResult> GetByPatient(int patientId) =>
            Ok(await _appointmentService.GetByPatientIdAsync(patientId));

        [HttpGet("doctor/{doctorId:int}")]
        public async Task<IActionResult> GetByDoctor(int doctorId) =>
            Ok(await _appointmentService.GetByDoctorIdAsync(doctorId));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetByIdAsync(id);
            return appointment is null ? NotFound() : Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentRequest request)
        {
            try
            {
                var appointment = await _appointmentService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateAppointmentRequest request)
        {
            var success = await _appointmentService.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _appointmentService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
