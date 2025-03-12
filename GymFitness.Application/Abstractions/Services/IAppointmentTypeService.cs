using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IAppointmentTypeService
    {
        Task<IEnumerable<AppointmentTypeResponseDto>> GetAllAppointmentTypesAsync(string? filterOn, 
                                                                       string filterQuery, 
                                                                       int pageNumber = 1, 
                                                                       int pageSize =10);
        Task<AppointmentType?> GetAppointmentTypeByIdAsync(int typeId);
        Task AddAppointmentTypeAsync(AppointmentType appointmentType);
        Task<AppointmentTypeResponseDto> UpdateAppointmentTypeAsync(AppointmentType appointmentType);
        Task DeleteAppointmentTypeAsync(int typeId);
      
    }
}
