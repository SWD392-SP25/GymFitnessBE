using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        public AppointmentController(IAppointmentService appointmentService, IUserService userService, IStaffService staffService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sortBy,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var appointments = await _appointmentService.GetAppointmentsAsync(
                filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(appointments);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentDto dto)
        {
            var user = await _userService.GetUserByEmail(dto.UserEmail);
            var staff = await _staffService.GetByEmailAsync(dto.StaffEmail);


            if (user == null || staff == null)
            {
                return BadRequest("User or Staff not found");
            }
            var appointment = new Appointment
            {
                UserId = user.UserId,
                StaffId = staff.StaffId,
                TypeId = dto.TypeId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = dto.Status,
                Notes = dto.Notes,
                Location = dto.Location,
                CreatedAt = dto.CreatedAt
            };

            await _appointmentService.AddAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentId }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentDto dto)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            var user = await _userService.GetUserByEmail(dto.UserEmail);
            var staff = await _staffService.GetByEmailAsync(dto.StaffEmail);

            if (user == null || staff == null)
            {
                return BadRequest("User or Staff not found");
            }
            appointment.UserId = user.UserId;
            appointment.StaffId = staff.StaffId;
            appointment.TypeId = dto.TypeId;
            appointment.StartTime = dto.StartTime;
            appointment.EndTime = dto.EndTime;
            appointment.Status = dto.Status;
            appointment.Notes = dto.Notes;
            appointment.Location = dto.Location;
            appointment.CreatedAt = dto.CreatedAt;

            await _appointmentService.UpdateAppointmentAsync(appointment);
            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }
    }
}
