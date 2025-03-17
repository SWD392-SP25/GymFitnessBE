using GymFitness.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionPlanController : Controller
    {
        private readonly ISubscriptionPlanService _subscriptionPlanService;

        public SubscriptionPlanController(ISubscriptionPlanService subscriptionPlanService)
        {
            _subscriptionPlanService = subscriptionPlanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptionPlan([FromQuery] string? filterOn,
                                                            [FromQuery] string? filterQuery,
                                                            [FromQuery] string? sortBy,
                                                            [FromQuery] bool? isAscending,
                                                            [FromQuery] int pageNumber = 1,
                                                            [FromQuery] int pageSize = 10)
        {
            var subscriptionPlans = await _subscriptionPlanService.GetSubscriptionPlan(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return Ok(subscriptionPlans);
        }
    }
}
