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
    public class StaffSpecializationController : ControllerBase
    {
        private readonly IStaffSpecializationService _service;

        public StaffSpecializationController(IStaffSpecializationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sortBy,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 10)
        {
            var specializations = await _service.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(specializations);
        }

        [HttpGet("{staffId}/{specializationId}")]
        public async Task<IActionResult> GetById(Guid staffId, int specializationId)
        {
            var specialization = await _service.GetByIdAsync(staffId, specializationId);
            if (specialization == null)
                return NotFound();
            return Ok(specialization);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffSpecialization staffSpecialization)
        {
            await _service.AddAsync(staffSpecialization);
            return CreatedAtAction(nameof(GetById), new { staffId = staffSpecialization.StaffId, specializationId = staffSpecialization.SpecializationId }, staffSpecialization);
        }

        [HttpPut("{staffId}/{specializationId}")]
        public async Task<IActionResult> Update(Guid staffId, int specializationId, [FromBody] StaffSpecialization staffSpecialization)
        {
            if (staffId != staffSpecialization.StaffId || specializationId != staffSpecialization.SpecializationId)
                return BadRequest("Invalid specialization data.");

            await _service.UpdateAsync(staffSpecialization);
            return NoContent();
        }

        [HttpDelete("{staffId}/{specializationId}")]
        public async Task<IActionResult> Delete(Guid staffId, int specializationId)
        {
            await _service.DeleteAsync(staffId, specializationId);
            return NoContent();
        }
    }
}
