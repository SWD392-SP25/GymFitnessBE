using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaffs([FromQuery] string? filterOn, 
                                                      [FromQuery] string? filterQuery,
                                                      [FromQuery] string? sortOn,
                                                      [FromQuery] bool? isAscending,
                                                      [FromQuery] int pageNumber = 1,
                                                      [FromQuery] int pageSize = 10)
        {
            var staff =  await _staffService.GetAllStaffsAsync(filterOn, filterQuery,sortOn, isAscending ?? true, pageNumber, pageSize);
            return Ok(staff);
        }
            

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(Guid id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
            if (staff == null) return NotFound();
            return staff;
        }
        [HttpGet("/email/{email}")]
        public async Task<ActionResult<Staff>> GetStaffByEmail(string email)
        {
            var staff = await _staffService.GetByEmailAsync(email);
            if (staff == null) return NotFound();
            return staff;
        }

        [HttpPost]
        public async Task<IActionResult> AddStaff(Staff staff)
        {
            await _staffService.AddStaffAsync(staff);
            return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(Guid id, [FromBody] Staff staff)
        {
            if (staff == null || id != staff.StaffId)
            {
                return BadRequest("Invalid staff data.");
            }

            var existingStaff = await _staffService.GetStaffByIdAsync(id);
            if (existingStaff == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin staff
            existingStaff.Email = staff.Email;
            existingStaff.FirstName = staff.FirstName;
            existingStaff.LastName = staff.LastName;
            existingStaff.RoleId = staff.RoleId;
            existingStaff.Phone = staff.Phone;
            existingStaff.Salary = staff.Salary;    
            existingStaff.Status = staff.Status;
            existingStaff.Department = staff.Department;

            await _staffService.UpdateStaffAsync(existingStaff);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(Guid id)
        {
            await _staffService.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
