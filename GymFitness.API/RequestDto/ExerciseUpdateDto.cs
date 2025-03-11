namespace GymFitness.API.Dto
{
    public class ExerciseUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? MuscleGroupId { get; set; }
        public int? CategoryId { get; set; }
        public int? DifficultyLevel { get; set; }
        public string? EquipmentNeeded { get; set; }
        public IFormFile? ImageFile { get; set; }  // File ảnh mới (nếu có)
        public IFormFile? VideoFile { get; set; }  // File video mới (nếu có)
        public string? Instructions { get; set; }
        public string? Precautions { get; set; }
    }

}
