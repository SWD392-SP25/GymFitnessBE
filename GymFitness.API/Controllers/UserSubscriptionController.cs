using Azure;
using GymFitness.API.RequestDto;
using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : Controller
    {
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IUserService _userService;

        public UserSubscriptionController(IUserSubscriptionService userSubscriptionService, IUserService userService)
        {
            _userSubscriptionService = userSubscriptionService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubscriptions([FromQuery] string? filterOn,
                                                             [FromQuery] string? filterQuery,
                                                             [FromQuery] int pageNumber = 1,
                                                             [FromQuery] int pageSize = 10)
        {
            var userSubscriptions = await _userSubscriptionService.GetUserSubscriptions(filterOn, filterQuery, pageNumber, pageSize);
            return Ok(userSubscriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSubscriptionById(int id)
        {
            var userSubscription = await _userSubscriptionService.GetUserSubscriptionById(id);
            if (userSubscription == null)
                return NotFound();
            return Ok(userSubscription);
        }

        [HttpPatch("{id}")]
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<UserSubscriptionDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var userSubscription = await _userSubscriptionService.GetUserSubscriptionById(id);
            if (userSubscription == null)
                return NotFound();

            var userSubscriptionDto = new UserSubscriptionDto
            {
                SubscriptionPlanId = userSubscription.SubscriptionPlanId,
                StartDate = userSubscription.StartDate,
                EndDate = userSubscription.EndDate,
                Status = userSubscription.Status,
                PaymentFrequency = userSubscription.PaymentFrequency,
                AutoRenew = userSubscription.AutoRenew
            };

            patchDoc.ApplyTo(userSubscriptionDto, error =>
            {
                ModelState.AddModelError(error.AffectedObject.ToString(), error.ErrorMessage);
            });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Update userSubscription
            userSubscription.SubscriptionPlanId = userSubscriptionDto.SubscriptionPlanId;
            userSubscription.StartDate = userSubscriptionDto.StartDate;
            userSubscription.EndDate = userSubscriptionDto.EndDate;
            userSubscription.Status = userSubscriptionDto.Status;
            userSubscription.PaymentFrequency = userSubscriptionDto.PaymentFrequency;
            userSubscription.AutoRenew = userSubscriptionDto.AutoRenew;

            // Chặn cập nhật CreatedAt
            var updatedProperties = patchDoc.Operations
                .Select(o => o.path.TrimStart('/'))
                .Where(p => p != "CreatedAt")
                .ToList();

            await _userSubscriptionService.UpdateUserSubscription(userSubscription, updatedProperties);
            return Ok(userSubscription);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSubscriptionCreateDto dto)
        {
            var user = await _userService.GetUserByEmail(dto.Email);
            if (user == null)
                return BadRequest("User not found");
            var userSubscription = new UserSubscription
            {
                UserId = user.UserId,
                SubscriptionPlanId = dto.SubscriptionPlanId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                PaymentFrequency = dto.PaymentFrequency,
                AutoRenew = dto.AutoRenew,
                CreatedAt = DateTime.Now,
                Sub = dto.Sub
            };
            await _userSubscriptionService.AddUserSubcription(userSubscription);
            return Ok(userSubscription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userSubscription = await _userSubscriptionService.GetUserSubscriptionById(id);
            if(userSubscription == null)
            {
                return BadRequest("User subscription not found");
            }
            await _userSubscriptionService.DeleteUserSubscription(userSubscription);
            return Ok(userSubscription);
        }
    }
}
