using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseResponseDto>> GetAllExercisesAsync(string? filterOn,
                                                                    string? filterQuery,
                                                                    string? sortBy,
                                                                    bool? isAscending,
                                                                    int pageNumber = 1,
                                                                    int pageSize = 10);
        Task<Exercise?> GetExerciseByIdAsync(int id);
        Task AddExerciseAsync(Exercise exercise);
        Task UpdateExerciseAsync(Exercise exercise);
        Task DeleteExerciseAsync(int id);
    }
}
