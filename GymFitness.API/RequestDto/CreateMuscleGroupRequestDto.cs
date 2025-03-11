namespace GymFitness.API.Dto
{
    public class CreateMuscleGroupRequestDto
    {
        
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
