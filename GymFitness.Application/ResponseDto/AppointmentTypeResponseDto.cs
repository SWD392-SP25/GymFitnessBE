using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.ResponseDto
{
     public class AppointmentTypeResponseDto
    {
        public int TypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? DurationMinutes { get; set; }
        public decimal? Price { get; set; }
    }
}
