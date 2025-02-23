namespace GymFitness.API.Dto
{
    public class StaffSpecializationDto
    {
        public int StaffId { get; set; }
        public int SpecializationId { get; set; }
        public string? CertificationNumber { get; set; }
        public DateOnly? CertificationDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? VerificationStatus { get; set; }
    }
}
