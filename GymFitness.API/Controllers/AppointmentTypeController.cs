using GymFitness.Application.Dtos;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private readonly AppointmentTypeService _service;

        public AppointmentTypeController(AppointmentTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointmentTypes = await _service.GetAllAppointmentTypesAsync();
            return Ok(appointmentTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointmentType = await _service.GetAppointmentTypeByIdAsync(id);
            if (appointmentType == null)
                return NotFound();
            return Ok(appointmentType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentTypeDto dto)
        {
            var appointmentType = new AppointmentType
            {
                Name = dto.Name,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                Price = dto.Price
            };

            await _service.AddAppointmentTypeAsync(appointmentType);
            return CreatedAtAction(nameof(GetById), new { id = appointmentType.TypeId }, appointmentType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentTypeDto dto)
        {
            var appointmentType = await _service.GetAppointmentTypeByIdAsync(id);
            if (appointmentType == null)
                return NotFound();

            appointmentType.Name = dto.Name;
            appointmentType.Description = dto.Description;
            appointmentType.DurationMinutes = dto.DurationMinutes;
            appointmentType.Price = dto.Price;

            await _service.UpdateAppointmentTypeAsync(appointmentType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAppointmentTypeAsync(id);
            return NoContent();
        }
    }
}
