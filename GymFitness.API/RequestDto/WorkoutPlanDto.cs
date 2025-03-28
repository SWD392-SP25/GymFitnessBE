﻿namespace GymFitness.Application.Dtos
{
    public class WorkoutPlanDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? DifficultyLevel { get; set; }
        public int? DurationWeeks { get; set; }
        public string? CreatedBy { get; set; }
        public string? TargetAudience { get; set; }
        public string? Goals { get; set; }
        public string? Prerequisites { get; set; }
    }
}
