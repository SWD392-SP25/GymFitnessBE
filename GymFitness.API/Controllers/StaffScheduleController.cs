using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffScheduleController : ControllerBase
    {
        private readonly IStaffScheduleService _service;

        public StaffScheduleController(IStaffScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, 
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var schedules = await _service.GetAllSchedulesAsync(filterOn, filterQuery, pageNumber, pageSize);
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var schedule = await _service.GetScheduleByIdAsync(id);
            if (schedule == null)
                return NotFound();
            return Ok(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffScheduleDto dto)
        {
            var schedule = new StaffSchedule
            {
                StaffId = dto.StaffId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsAvailable = dto.IsAvailable,
                Location = dto.Location
            };

            await _service.AddScheduleAsync(schedule);
            return CreatedAtAction(nameof(GetById), new { id = schedule.ScheduleId }, schedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StaffScheduleDto dto)
        {
            var schedule = await _service.GetScheduleByIdAsync(id);
            if (schedule == null)
                return NotFound();

            schedule.StaffId = dto.StaffId;
            schedule.DayOfWeek = dto.DayOfWeek;
            schedule.StartTime = dto.StartTime;
            schedule.EndTime = dto.EndTime;
            schedule.IsAvailable = dto.IsAvailable;
            schedule.Location = dto.Location;

            await _service.UpdateScheduleAsync(schedule);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteScheduleAsync(id);
            return NoContent();
        }
    }
}
