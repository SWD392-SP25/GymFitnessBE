using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IExerciseRepository
    {
        Task<IEnumerable<Exercise>> GetAllAsync(string? filterOn,
                                                string? filterQuery,
                                                string? sortBy,
                                                bool isAscending,
                                                int pageNumber = 1,
                                                int pageSize = 10);
        Task<Exercise?> GetByIdAsync(int id);
        Task AddAsync(Exercise exercise);
        Task UpdateAsync(Exercise exercise, List<string> updatedProperties);
        Task DeleteAsync(int id);
    }
}
