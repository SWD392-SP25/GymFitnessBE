using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _workoutPlan;

        public WorkoutPlanService(IWorkoutPlanRepository workoutPlan)
        {
            _workoutPlan = workoutPlan;
        }

        public async Task<IEnumerable<WorkoutPlanResponseDto>> GetAllWorkoutPlansAsync(string? filterOn, string? filterQuery, string? sortBy, bool? isAscending, int pageNumber = 1, int pageSize = 10)
        {
            var plans = await _workoutPlan.GetAllAsync(filterOn, 
                                                       filterQuery, 
                                                       sortBy, 
                                                       isAscending, 
                                                       pageNumber, 
                                                       pageSize);
            return plans.Select(plan => new WorkoutPlanResponseDto
            {
                PlanName = plan.Name,
                PlantDescription = plan.Description,
                DifficultyLevel = plan.DifficultyLevel,
                DurationWeeks = plan.DurationWeeks,
                StaffEmail = plan.CreatedByNavigation?.Email,
                StaffName = plan.CreatedByNavigation?.LastName,
                TargetAudience = plan.TargetAudience,
                Goals = plan.Goals,
                Prerequisites = plan.Prerequisites,
                CreatedAt = plan.CreatedAt,

                // ✅ Map danh sách WorkoutPlanExercises (bài tập trong kế hoạch)
                Exercises = plan.WorkoutPlanExercises?.Select(wpEx => new WorkoutExerciseResponseDto
                {
                    ExerciseId = wpEx.Exercise.ExerciseId,
                    ExerciseName = wpEx.Exercise.Name,
                    Sets = wpEx.Sets,
                    Reps = wpEx.Reps,
                    RestTime = wpEx.RestTimeSeconds,
                    Notes = wpEx.Notes,

                    // Thông tin bổ sung từ Exercise
                    MuscleGroupName = wpEx.Exercise.MuscleGroup?.Name,
                    CategoryName = wpEx.Exercise.Category?.Name,
                    DifficultyLevel = wpEx.Exercise.DifficultyLevel,
                    EquipmentNeeded = wpEx.Exercise.EquipmentNeeded,
                    VideoUrl = wpEx.Exercise.VideoUrl
                }).ToList()
            }).ToList();

        }
 

        public async Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(int id) =>
            await _workoutPlan.GetByIdAsync(id);

        public async Task AddWorkoutPlanAsync(WorkoutPlan plan) =>
            await _workoutPlan.AddAsync(plan);

        public async Task UpdateWorkoutPlanAsync(WorkoutPlan plan) =>
            await _workoutPlan.UpdateAsync(plan);

        public async Task DeleteWorkoutPlanAsync(int id) =>
            await _workoutPlan.DeleteAsync(id);


    }
}
