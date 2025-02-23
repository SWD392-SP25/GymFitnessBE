using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IExerciseCategoryRepository
    {
        Task<IEnumerable<ExerciseCategory>> GetAllAsync();
        Task<ExerciseCategory?> GetByIdAsync(int id);
        Task AddAsync(ExerciseCategory category);
        Task UpdateAsync(ExerciseCategory category);
        Task DeleteAsync(int id);
    }
}
