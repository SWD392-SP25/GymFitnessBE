using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IExerciseCategoryService
    {
        Task<IEnumerable<ExerciseCategoryResponseDto>> GetAllCategoriesAsync(string? filterOn, 
                                                                  string? filterQuery, 
                                                                  int pageNumber = 1, 
                                                                  int pageSize = 10);
        Task<ExerciseCategory?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(ExerciseCategory category);
        Task<ExerciseCategoryResponseDto?> UpdateCategoryAsync(ExerciseCategory category);
        Task DeleteCategoryAsync(int id);
    }
}
