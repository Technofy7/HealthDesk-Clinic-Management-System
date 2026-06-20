using HealthDesk.Service;
using HealthDesk.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HealthDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        public MedicalRecordsController(IMedicalRecordService medicalRecordService) => _medicalRecordService = medicalRecordService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _medicalRecordService.GetAllAsync());

        [HttpGet("appointment/{appointmentId:int}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
        {
            var record = await _medicalRecordService.GetByAppointmentIdAsync(appointmentId);
            return record is null ? NotFound() : Ok(record);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _medicalRecordService.GetByIdAsync(id);
            return record is null ? NotFound() : Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMedicalRecordRequest request)
        {
            try
            {
                var record = await _medicalRecordService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateMedicalRecordRequest request)
        {
            var success = await _medicalRecordService.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _medicalRecordService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
