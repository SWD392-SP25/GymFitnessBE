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
        Task<IEnumerable<AppointmentType>> GetAllAppointmentTypesAsync();
        Task<AppointmentType?> GetAppointmentTypeByIdAsync(int typeId);
        Task AddAppointmentTypeAsync(AppointmentType appointmentType);
        Task UpdateAppointmentTypeAsync(AppointmentType appointmentType);
        Task DeleteAppointmentTypeAsync(int typeId);
    }
}
