using HealthDesk.Service;
using HealthDesk.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HealthDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSlotsController : ControllerBase
    {
        private readonly ITimeSlotService _timeSlotService;
        public TimeSlotsController(ITimeSlotService timeSlotService) => _timeSlotService = timeSlotService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _timeSlotService.GetAllAsync());

        [HttpGet("doctor/{doctorId:int}")]
        public async Task<IActionResult> GetByDoctor(int doctorId) =>
            Ok(await _timeSlotService.GetByDoctorIdAsync(doctorId));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slot = await _timeSlotService.GetByIdAsync(id);
            return slot is null ? NotFound() : Ok(slot);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTimeSlotRequest request)
        {
            try
            {
                var slot = await _timeSlotService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = slot.Id }, slot);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateTimeSlotRequest request)
        {
            try
            {
                var success = await _timeSlotService.UpdateAsync(id, request);
                return success ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _timeSlotService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
