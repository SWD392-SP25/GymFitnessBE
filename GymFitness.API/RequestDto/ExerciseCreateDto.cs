namespace GymFitness.API.Dto
{
    public class ExerciseCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? MuscleGroupId { get; set; }
        public int? CategoryId { get; set; }
        public string? DifficultyLevel { get; set; }
        public string? EquipmentNeeded { get; set; }
        public IFormFile? ImageFile { get; set; }  // File ảnh
        public IFormFile? VideoFile { get; set; }  // File video
        public string? Instructions { get; set; }
        public string? Precautions { get; set; }
    }

}
