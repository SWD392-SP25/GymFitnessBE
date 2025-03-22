using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class WorkoutPlanExerciseService : IWorkoutPlanExerciseService
    {
        private readonly IWorkoutPlanExerciseRepository _workoutPlanExerciseRepository;

        public WorkoutPlanExerciseService(IWorkoutPlanExerciseRepository workoutPlanExerciseRepository)
        {
            _workoutPlanExerciseRepository = workoutPlanExerciseRepository;
        }
        public async Task AddAsync(WorkoutPlanExercise input)
        {
            await _workoutPlanExerciseRepository.AddAsync(input);
        }

        public async Task DeleteAsync(int workoutPlanId)
        {
            await _workoutPlanExerciseRepository.DeleteAsync(workoutPlanId);
        }

        public async Task<WorkoutPlanExercise?> GetWorkoutPlanExerciseByIdAsync(int workoutPlanId)
        {
            var workoutPlan = await _workoutPlanExerciseRepository.GetWorkoutPlanExerciseByIdAsync(workoutPlanId);
            if (workoutPlan == null)
            {
                return null;
            }
            return workoutPlan;
        }

        public async Task<List<WorkoutPlanExerciseResponseDto>> GetWorkoutPlanExercisesAsync(string? filterOn, string? filterQuery, string sortBy = "difficulty", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var workoutPlanExercises = await _workoutPlanExerciseRepository.GetWorkoutPlanExercisesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            Console.WriteLine($"Workout plans exercise found at Service: {workoutPlanExercises.ToList().Count}");
            return workoutPlanExercises.Select(wpe => new WorkoutPlanExerciseResponseDto
            {
                WorkoutPlanId = wpe.PlanId,
                WeekNumber = wpe.WeekNumber,
                DayOfWeek = wpe.DayOfWeek,
                Sets = wpe.Sets,
                Reps = wpe.Reps,
                RestTimeSeconds = wpe.RestTimeSeconds,
                Notes = wpe.Notes,
                Exercise = new ExerciseResponseDto 
                {
                    ExerciseId = wpe.Exercise.ExerciseId,
                    Name = wpe.Exercise.Name,
                    Description = wpe.Exercise.Description,
                    MuscleGroupName = wpe.Exercise.MuscleGroup.Name ?? "Unknow",
                    CategoryName = wpe.Exercise.Category.Name ?? "Unknow",
                    DifficultyLevel = wpe.Exercise.DifficultyLevel,
                    EquipmentNeeded = wpe.Exercise.EquipmentNeeded,
                    VideoUrl = wpe.Exercise.VideoUrl,
                    ImageUrl = wpe.Exercise.ImageUrl,
                    Instructions = wpe.Exercise.Instructions,
                    Precautions = wpe.Exercise.Precautions,
                    CreatedAt = wpe.Exercise.CreatedAt
                }
            }).ToList();
        }

        public Task UpdateAsync(WorkoutPlanExercise input, List<string> updatedProperties)
        {
            throw new NotImplementedException();
        }
    }
}
