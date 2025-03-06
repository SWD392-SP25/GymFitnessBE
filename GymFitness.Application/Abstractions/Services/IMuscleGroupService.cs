using GymFitness.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IMuscleGroupService
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
