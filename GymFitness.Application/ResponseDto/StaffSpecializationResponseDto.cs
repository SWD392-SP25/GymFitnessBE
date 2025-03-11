using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
    public class StaffSpecializationResponseDto
    {
        public string StaffEmail { get; set; }
        public string? SpecializationName { get; set; }
        public string? CertificationNumber { get; set; }
        public DateOnly? CertificationDate { get; set; }
        public DateOnly? ExpirityDate { get; set; }
        public string? Description { get; set; }
    }
}
