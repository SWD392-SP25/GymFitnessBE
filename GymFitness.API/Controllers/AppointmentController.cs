using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Dtos;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] AppointmentDto dto)
        //{
        //    var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        //    if (appointment == null)
        //        return NotFound();

        //    var user = await _userService.GetUserByEmail(dto.UserEmail);
        //    var staff = await _staffService.GetByEmailAsync(dto.StaffEmail);

        //    if (user == null || staff == null)
        //    {
        //        return BadRequest("User or Staff not found");
        //    }
        //    appointment.UserId = user.UserId;
        //    appointment.StaffId = staff.StaffId;
        //    appointment.TypeId = dto.TypeId;
        //    appointment.StartTime = dto.StartTime;
        //    appointment.EndTime = dto.EndTime;
        //    appointment.Status = dto.Status;
        //    appointment.Notes = dto.Notes;
        //    appointment.Location = dto.Location;
        //    appointment.CreatedAt = dto.CreatedAt;

        //    await _appointmentService.UpdateAppointmentAsync(appointment);
        //    return Ok(appointment);
        //}

        [HttpPatch("{id}")]
        
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<AppointmentDto> patchDoc)
        {
            // Kiểm tra Patch document có null không
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null");
            }

            // Lấy thông tin appointment từ id
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            // Chuyển entity sang DTO để cập nhật
            var appointmentDto = new AppointmentDto
            {
                UserEmail = appointment.User?.Email,
                StaffEmail = appointment.Staff?.Email,
                TypeId = appointment.TypeId,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                Status = appointment.Status,
                Notes = appointment.Notes,
                Location = appointment.Location,
                CreatedAt = appointment.CreatedAt
            };

            // Áp dụng JSON Patch với callback xử lý lỗi
            patchDoc.ApplyTo(appointmentDto, error =>
            {
                ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage);
            });

            // Kiểm tra lỗi sau khi áp dụng Patch
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy lại thông tin User và Staff từ DTO
            var user = appointmentDto.UserEmail != null ? await _userService.GetUserByEmail(appointmentDto.UserEmail) : null;
            var staff = appointmentDto.StaffEmail != null ? await _staffService.GetByEmailAsync(appointmentDto.StaffEmail) : null;

            if (appointmentDto.UserEmail != null && user == null)
            {
                return BadRequest("User not found");
            }

            if (appointmentDto.StaffEmail != null && staff == null)
            {
                return BadRequest("Staff not found");
            }

            // Cập nhật lại dữ liệu vào entity
            appointment.UserId = user?.UserId ?? appointment.UserId;
            appointment.StaffId = staff?.StaffId ?? appointment.StaffId;
            appointment.TypeId = appointmentDto.TypeId;
            appointment.StartTime = appointmentDto.StartTime;
            appointment.EndTime = appointmentDto.EndTime;
            appointment.Status = appointmentDto.Status;
            appointment.Notes = appointmentDto.Notes;
            appointment.Location = appointmentDto.Location;

            // Chặn cập nhật CreatedAt
            var updatedProperties = patchDoc.Operations
                .Select(o => o.path.TrimStart('/'))
                .Where(p => p != "CreatedAt")
                .ToList();

            await _appointmentService.UpdateAppointmentAsync(appointment, updatedProperties);

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
