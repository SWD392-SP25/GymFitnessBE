using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymFitness.Infrastructure.Data;

[Route("api/[controller]")]
[ApiController]
public class StaffsController : ControllerBase
{
    private readonly GymbotDbContext _context;

    public StaffsController(GymbotDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStaffs()
    {
        var staffs = await _context.Staffs.ToListAsync();
        return Ok(staffs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStaffById(int id)
    {
        var staff = await _context.Staffs.FindAsync(id);
        if (staff == null) return NotFound();
        return Ok(staff);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStaff([FromBody] Staff staff)
    {
        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStaff(int id, [FromBody] Staff staff)
    {
        if (id != staff.StaffId) return BadRequest();
        _context.Entry(staff).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var staff = await _context.Staffs.FindAsync(id);
        if (staff == null) return NotFound();
        _context.Staffs.Remove(staff);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
