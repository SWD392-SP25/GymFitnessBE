using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories;

public class SubscriptionPlanRepository : ISubscriptionPlanRepository
{
    private readonly GymbotDbContext _context;

    public SubscriptionPlanRepository(GymbotDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SubscriptionPlan>> GetAllAsync() =>
        await _context.SubscriptionPlans.ToListAsync();

    public async Task<SubscriptionPlan?> GetByIdAsync(int id) =>
        await _context.SubscriptionPlans.FindAsync(id);

    public async Task AddAsync(SubscriptionPlan plan)
    {
        _context.SubscriptionPlans.Add(plan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SubscriptionPlan plan)
    {
        _context.SubscriptionPlans.Update(plan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var plan = await _context.SubscriptionPlans.FindAsync(id);
        if (plan != null)
        {
            _context.SubscriptionPlans.Remove(plan);
            await _context.SaveChangesAsync();
        }
    }
}
