using HealthDesk.Service;
using HealthDesk.Service.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthDesk.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService) => _doctorService = doctorService;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _doctorService.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return doctor is null ? NotFound() : Ok(doctor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDoctorRequest request)
        {
            try
            {
                var doctor = await _doctorService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateDoctorRequest request)
        {
            var success = await _doctorService.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _doctorService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
