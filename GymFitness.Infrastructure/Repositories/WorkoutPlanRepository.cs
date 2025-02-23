using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private readonly GymbotDbContext _context;

        public WorkoutPlanRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutPlan>> GetAllAsync() =>
            await _context.WorkoutPlans.ToListAsync();

        public async Task<WorkoutPlan?> GetByIdAsync(int id) =>
            await _context.WorkoutPlans.FindAsync(id);

        public async Task AddAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Add(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Update(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan != null)
            {
                _context.WorkoutPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }
    }
}
