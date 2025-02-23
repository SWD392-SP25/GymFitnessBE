namespace GymFitness.API.Dto
{
    public class ExerciseDto
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? MuscleGroupId { get; set; }
        public int? CategoryId { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? EquipmentNeeded { get; set; }
        public string? VideoUrl { get; set; }
        public string? ImageUrl { get; set; }
        public string? Instructions { get; set; }
        public string? Precautions { get; set; }
    }
}
