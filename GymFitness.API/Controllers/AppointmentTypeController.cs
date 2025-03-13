using Azure;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Dtos;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymFitness.API.RequestDto;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Staff,Admin")]
    public class AppointmentTypeController : ControllerBase
    {
        private readonly IAppointmentTypeService _service;

        public AppointmentTypeController(IAppointmentTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var appointmentTypes = await _service.GetAllAppointmentTypesAsync(filterOn, filterQuery, pageNumber, pageSize);
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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] AppointmentTypeDto dto)
        //{
        //    var appointmentType = await _service.GetAppointmentTypeByIdAsync(id);
        //    if (appointmentType == null)
        //        return NotFound();

        //    appointmentType.Name = dto.Name;
        //    appointmentType.Description = dto.Description;
        //    appointmentType.DurationMinutes = dto.DurationMinutes;
        //    appointmentType.Price = dto.Price;

        //    await _service.UpdateAppointmentTypeAsync(appointmentType);
        //    return NoContent();
        //}
        [HttpPatch("{id}")]
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<AppointmentTypeDto> patchDoc)
        {
            // **Kiểm tra xem patchDoc có null không**
            if (patchDoc == null)
            {
                return BadRequest("Patch document is null");
            }

            var appointmentType = await _service.GetAppointmentTypeByIdAsync(id);
            if (appointmentType == null)
            {
                return NotFound();
            }

            var appointmentTypeDto = new AppointmentTypeDto
            {
                Name = appointmentType.Name,
                Description = appointmentType.Description,
                DurationMinutes = appointmentType.DurationMinutes,
                Price = appointmentType.Price
            };

            // ** Áp dụng JSON Patch với callback xử lý lỗi**
            patchDoc.ApplyTo(appointmentTypeDto, error =>
            {
               ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage);
            });

            // ** Kiểm tra lỗi sau khi áp dụng Patch**
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ** Cập nhật dữ liệu**
            appointmentType.Name = appointmentTypeDto.Name;
            appointmentType.Description = appointmentTypeDto.Description;
            appointmentType.DurationMinutes = appointmentTypeDto.DurationMinutes;
            appointmentType.Price = appointmentTypeDto.Price;

            // ** Lưu thay đổi**
            var updatedAppointment = await _service.UpdateAppointmentTypeAsync(appointmentType);
            return Ok(updatedAppointment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAppointmentTypeAsync(id);
            return NoContent();
        }


    }
}
