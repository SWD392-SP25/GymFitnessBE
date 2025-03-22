namespace GymFitness.Application.Dtos
{
    public class WorkoutPlanExerciseDto
    {
        //public int PlanId { get; set; }
        public int ExerciseId { get; set; }
        public int WeekNumber { get; set; }
        public int DayOfWeek { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public int? RestTimeSeconds { get; set; }
        public string? Notes { get; set; }
    }
}
