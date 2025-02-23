using GymFitness.Application.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;

        public StaffController(StaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IEnumerable<Staff>> GetAllStaffs() =>
            await _staffService.GetAllStaffsAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            var staff = await _staffService.GetStaffByIdAsync(id);
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
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] Staff staff)
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
        public async Task<IActionResult> DeleteStaff(int id)
        {
            await _staffService.DeleteStaffAsync(id);
            return NoContent();
        }
    }
}
