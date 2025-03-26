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
    public class WorkoutPlanService : IWorkoutPlanService
    {
        private readonly IWorkoutPlanRepository _workoutPlanRepository;

        public WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository)
        {
            _workoutPlanRepository = workoutPlanRepository;
        }
        public async Task AddWorkoutPlan(WorkoutPlan workoutPlan)
        {
            await _workoutPlanRepository.AddAsync(workoutPlan);
        }

        public async Task DeleteWorkoutPlan(int workoutPlanId)
        {
            await _workoutPlanRepository.DeleteAsync(workoutPlanId);
        }

        public Task<WorkoutPlan?> GetWorkoutPlanById(int workoutPlanId)
        {
            var isExist = _workoutPlanRepository.GetWorkoutPlanByIdAsync(workoutPlanId);
            if (isExist == null)
            {
                return null;
            }
            return isExist;
        }

        public async Task<List<WorkoutPlanResponseDto>> GetWorkoutPlans(string? filterOn, string? filterQuery, string sortBy = "difficulty", bool? isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var workoutPlans = await  _workoutPlanRepository.GetWorkoutPlansAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            var result = workoutPlans.Select(wp => new WorkoutPlanResponseDto
            {
                PlanId = wp.PlanId,
                PlanName = wp.Name,
                PlantDescription = wp.Description,
                DifficultyLevel = wp.DifficultyLevel,
                DurationWeeks = wp.DurationWeeks,
                StaffEmail = wp.CreatedByNavigation.Email,
          
                TargetAudience = wp.TargetAudience,
                Goals = wp.Goals,
                Prerequisites = wp.Prerequisites,
                CreatedAt = wp.CreatedAt,
                SubscriptionPlanId = wp.SubscriptionPlanId,
                WorkoutPlanExercises = wp.WorkoutPlanExercises.Select(wpe => new WorkoutPlanExerciseResponseDto
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
                }).ToList()
            }).ToList();
            return result;
        }

        public async Task<List<WorkoutPlan?>> GetWorkoutPlansByStaff(Guid staffId)
        {
            return await _workoutPlanRepository.GetWorkoutPlansByStaff(staffId);
        }

        public async Task<List<WorkoutPlan?>> GetWorkoutPlansBySubscriptionPlanId(int subscriptionPlanId)
        {
            return await _workoutPlanRepository.GetWorkoutPlansBySubscriptionPlanIdAsync(subscriptionPlanId);
        }

        public async Task UpdateWorkoutPlan(WorkoutPlan workoutPlan, List<string> updatedProperties)
        {
            await _workoutPlanRepository.UpdateAsync(workoutPlan, updatedProperties);
        }

        
    }
}
