using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IAppointmentTypeRepository
    {
        Task<IEnumerable<AppointmentType>> GetAllAsync();
        Task<AppointmentType?> GetByIdAsync(int typeId);
        Task AddAsync(AppointmentType appointmentType);
        Task UpdateAsync(AppointmentType appointmentType);
        Task DeleteAsync(int typeId);
    }
}
