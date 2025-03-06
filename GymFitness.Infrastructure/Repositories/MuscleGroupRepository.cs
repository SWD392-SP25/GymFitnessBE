using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class MuscleGroupRepository : IMuscleGroupRepository
    {
        private readonly GymbotDbContext _context;

        public MuscleGroupRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MuscleGroup>> GetAllAsync(string? filterOn,
                                                                string? filterQuery,
                                                                int pageNumber = 1,
                                                                int pageSize = 10)
        {
            var muscleGroups = _context.MuscleGroups.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                muscleGroups = filterOn switch
                {
                    "name" => muscleGroups.Where(m => EF.Functions.Like(m.Name, filterQuery)),
                    "description" => muscleGroups.Where(m => EF.Functions.Like(m.Description, filterQuery)),
                    _ => muscleGroups
                };
            }
            // **Phân trang**
            muscleGroups = muscleGroups.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return await muscleGroups.ToListAsync();
        }
            

        public async Task<MuscleGroup?> GetByIdAsync(int id) =>
            await _context.MuscleGroups.FindAsync(id);

        public async Task AddAsync(MuscleGroup muscleGroup)
        {
            _context.MuscleGroups.Add(muscleGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MuscleGroup muscleGroup)
        {
            _context.MuscleGroups.Update(muscleGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var muscleGroup = await _context.MuscleGroups.FindAsync(id);
            if (muscleGroup != null)
            {
                _context.MuscleGroups.Remove(muscleGroup);
                await _context.SaveChangesAsync();
            }
        }
    }
}
