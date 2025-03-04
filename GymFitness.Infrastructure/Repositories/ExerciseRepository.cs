using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly GymbotDbContext _context;

        public ExerciseRepository(GymbotDbContext context)
        {
            _context = context;
        }

        //public Task<IEnumerable<Exercise>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 1000)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<Exercise>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var exercises = _context.Exercises
                .Include(e => e.Category)
                .Include(e => e.MuscleGroup)
                .AsQueryable();
            // **Lọc theo filterOn và filterQuery**
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                exercises = filterOn switch
                {
                    "name" => exercises.Where(e => e.Name.ToLower().Contains(filterQuery)),
                    "category" => exercises.Where(e => e.Category != null && e.Category.Name.ToLower().Contains(filterQuery)),
                    "muscleGroup" => exercises.Where(e => e.MuscleGroup != null && e.MuscleGroup.Name.ToLower().Contains(filterQuery)),
                    _ => exercises
                };
            }

            // **Sắp xếp linh hoạt theo `sortBy`**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.ToLower();
                exercises = sortBy switch
                {
                    "name" => isAscending
                        ? exercises.OrderBy(e => e.Name)
                        : exercises.OrderByDescending(e => e.Name),
                    "category" => isAscending
                        ? exercises.OrderBy(e => e.Category != null ? e.Category.Name : "")
                        : exercises.OrderByDescending(e => e.Category != null ? e.Category.Name : ""),
                    "muscleGroup" => isAscending
                        ? exercises.OrderBy(e => e.MuscleGroup != null ? e.MuscleGroup.Name : "")
                        : exercises.OrderByDescending(e => e.MuscleGroup != null ? e.MuscleGroup.Name : ""),
                    _ => exercises
                };
            }
            // **Phân trang**
            exercises = exercises.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await exercises.ToListAsync();

        }
        public async Task<IEnumerable<Exercise>> GetAllAsync() =>
            await _context.Exercises.Include(e => e.Category).Include(e => e.MuscleGroup).ToListAsync();

        public async Task<Exercise?> GetByIdAsync(int id) =>
            await _context.Exercises.FindAsync(id);

        public async Task AddAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }
        }

       
    }
}
