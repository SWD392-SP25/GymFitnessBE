using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class WorkoutPlanExerciseRepository : IWorkoutPlanExerciseRepository
    {
        private readonly GymbotDbContext _context;

        public WorkoutPlanExerciseRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutPlanExercise>> GetAllAsync() =>
            await _context.WorkoutPlanExercises.ToListAsync();

        public async Task<WorkoutPlanExercise?> GetByIdAsync(int planId, int exerciseId) =>
            await _context.WorkoutPlanExercises
                .FindAsync(planId, exerciseId);

        public async Task AddAsync(WorkoutPlanExercise workoutPlanExercise)
        {
            _context.WorkoutPlanExercises.Add(workoutPlanExercise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkoutPlanExercise workoutPlanExercise)
        {
            _context.WorkoutPlanExercises.Update(workoutPlanExercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int planId, int exerciseId)
        {
            var entity = await _context.WorkoutPlanExercises.FindAsync(planId, exerciseId);
            if (entity != null)
            {
                _context.WorkoutPlanExercises.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
