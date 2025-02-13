using Microsoft.AspNetCore.Mvc;
using GymFitness.Infrastructure; // Đảm bảo namespace đúng
using GymFitness.Domain; // Kiểm tra lại namespace
using Microsoft.EntityFrameworkCore;
using GymFitness.Infrastructure.Data;

[Route("api/[controller]")]
[ApiController]
public class SubscriptionsController : ControllerBase
{
    private readonly GymbotDbContext _context;

    public SubscriptionsController(GymbotDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubscriptions()
    {
        var subscriptions = await _context.SubscriptionPlans.ToListAsync();
        return Ok(subscriptions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubscriptionById(int id)
    {
        var subscription = await _context.SubscriptionPlans.FindAsync(id);
        if (subscription == null) return NotFound();
        return Ok(subscription);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionPlan subscription)
    {
        _context.SubscriptionPlans.Add(subscription);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSubscriptionById), new { id = subscription.PlanId }, subscription);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubscription(int id, [FromBody] SubscriptionPlan subscription)
    {
        if (id != subscription.PlanId) return BadRequest();
        _context.Entry(subscription).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubscription(int id)
    {
        var subscription = await _context.SubscriptionPlans.FindAsync(id);
        if (subscription == null) return NotFound();
        _context.SubscriptionPlans.Remove(subscription);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
