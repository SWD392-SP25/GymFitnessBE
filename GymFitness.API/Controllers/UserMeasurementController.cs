using GymFitness.API.Dto;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMeasurementController : Controller
    {
        private readonly IUserMeasurementService _service;

        public UserMeasurementController(IUserMeasurementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userMeasurements = await _service.GetUserMeasurementsAsync();
            return Ok(userMeasurements);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var userMeasurement = await _service.GetUserMeasurementByIdAsync(userId);
            if (userMeasurement == null)
                return NotFound();
            return Ok(userMeasurement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddUserMeasurementRequestDto dto)
        {
            var userMeasurement = new UserMeasurement
            {
                UserId = dto.UserId,
                Date = dto.Date,
                Weight = dto.Weight,
                Height = dto.Height,
                BodyFatPercentage = dto.Body_Fat,
                HipsCm = dto.Hips_cm,
                ArmsCm = dto.Arm_cm,
                ChestCm = dto.Chest_cm,
                WaistCm = dto.Waist_cm,
                ThighsCm = dto.Thigh_cm

            };
            await _service.AddUserMeasurementAsync(userMeasurement);
            return CreatedAtAction(nameof(GetById), new { userId = userMeasurement.UserId }, userMeasurement);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AddUserMeasurementRequestDto dto)
        {
            var userMeasurement = new UserMeasurement
            {
                UserId = dto.UserId,
                Date = dto.Date,
                Weight = dto.Weight,
                Height = dto.Height,
                BodyFatPercentage = dto.Body_Fat,
                HipsCm = dto.Hips_cm,
                ArmsCm = dto.Arm_cm,
                ChestCm = dto.Chest_cm,
                WaistCm = dto.Waist_cm,
                ThighsCm = dto.Thigh_cm
            };
            await _service.UpdateUserMeasurementAsync(userMeasurement);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            await _service.DeleteUserMeasurementAsync(userId);
            return Ok();
        }
    }
}
