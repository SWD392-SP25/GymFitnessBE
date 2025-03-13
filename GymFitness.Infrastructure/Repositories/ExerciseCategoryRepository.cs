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

        public async Task<IEnumerable<ExerciseCategory>> GetAllAsync(string? filterOn,
                                                                     string? filterQuery,
                                                                     int pageNumber = 1,
                                                                     int pageSize = 10)
        {
            var categories =  _context.ExerciseCategories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                categories = filterOn switch
                {
                    "name" => categories.Where(c => EF.Functions.Like(c.Name, filterQuery)),
                    "description" => categories.Where(c => EF.Functions.Like(c.Description, filterQuery)),
                    _ => categories
                };
            }
            // **Phân trang**
            categories = categories.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await categories.ToListAsync();
        }
           

        public async Task<ExerciseCategory?> GetByIdAsync(int id) =>
            await _context.ExerciseCategories.FindAsync(id);

        public async Task AddAsync(ExerciseCategory category)
        {
            _context.ExerciseCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<ExerciseCategory?> UpdateAsync(ExerciseCategory category)
        {
            _context.ExerciseCategories.Update(category);
            await _context.SaveChangesAsync();
            return category;
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