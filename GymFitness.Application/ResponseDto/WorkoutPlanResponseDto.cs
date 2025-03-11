using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class WorkoutPlanResponseDto
    {
        public string? PlanName { get; set; }
        public string? PlantDescription { get; set; }
        public int? DifficultyLevel { get; set; }
        public int? DurationWeeks { get; set; }
        public string? StaffEmail { get; set; }
        public string? StaffName { get; set; }
        public string? TargetAudience { get; set; }
        public string? Goals { get; set; }
        public string? Prerequisites { get; set; }
        public DateTime? CreatedAt { get; set; }


        public List<WorkoutExerciseResponseDto>? Exercises { get; set; }
    }
}
