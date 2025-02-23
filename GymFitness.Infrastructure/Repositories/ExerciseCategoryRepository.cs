using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class ExerciseCategoryRepository : IExerciseCategoryRepository
    {
        private readonly GymbotDbContext _context;

        public ExerciseCategoryRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExerciseCategory>> GetAllAsync() =>
            await _context.ExerciseCategories.ToListAsync();

        public async Task<ExerciseCategory?> GetByIdAsync(int id) =>
            await _context.ExerciseCategories.FindAsync(id);

        public async Task AddAsync(ExerciseCategory category)
        {
            _context.ExerciseCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExerciseCategory category)
        {
            _context.ExerciseCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.ExerciseCategories.FindAsync(id);
            if (category != null)
            {
                _context.ExerciseCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}