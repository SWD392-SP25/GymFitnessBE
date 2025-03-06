using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IMuscleGroupRepository
    {
        Task<IEnumerable<MuscleGroup>> GetAllAsync(string? filterOn,
                                                   string? filterQuery,
                                                   int pageNumber = 1,
                                                   int pageSize = 10);
        Task<MuscleGroup?> GetByIdAsync(int id);
        Task AddAsync(MuscleGroup muscleGroup);
        Task UpdateAsync(MuscleGroup muscleGroup);
        Task DeleteAsync(int id);
    }
}
