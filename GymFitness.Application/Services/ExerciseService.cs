using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exercise;

        public ExerciseService(IExerciseRepository exercise)
        {
            _exercise = exercise;
        }

        public async Task<IEnumerable<ExerciseResponseDto>> GetAllExercisesAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber, int pageSize) 
        {
            var exercises = await _exercise.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return exercises.Select(a => new ExerciseResponseDto
            {
                ExerciseId = a.ExerciseId,
                Name = a.Name,
                Description = a.Description,
                CategoryName = a.Category != null ? a.Category.Name : null,
                MuscleGroupName = a.MuscleGroup != null ? a.MuscleGroup.Name : null,
                DifficultyLevel = a.DifficultyLevel,
                EquipmentNeeded = a.EquipmentNeeded,
                ImageUrl = a.ImageUrl,
                VideoUrl = a.VideoUrl,
                Instructions = a.Instructions,
                Precautions = a.Precautions,
                CreatedAt = a.CreatedAt
            });
        }
       
        public async Task<Exercise?> GetExerciseByIdAsync(int id) => await _exercise.GetByIdAsync(id);
        public async Task AddExerciseAsync(Exercise exercise) => await _exercise.AddAsync(exercise);
        public async Task UpdateExerciseAsync(Exercise exercise, List<string> updatedProperties) => await _exercise.UpdateAsync(exercise, updatedProperties);
        public async Task DeleteExerciseAsync(int id) => await _exercise.DeleteAsync(id);
    }
}
