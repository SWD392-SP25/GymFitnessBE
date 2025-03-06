using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IAppointmentTypeRepository
    {
        Task<IEnumerable<AppointmentType>> GetAllAsync(string? filterOn, string? filterQuery, int pageNumber, int pageSize);
        Task<AppointmentType?> GetByIdAsync(int typeId);
        Task AddAsync(AppointmentType appointmentType);
        Task UpdateAsync(AppointmentType appointmentType);
        Task DeleteAsync(int typeId);
        Task<AppointmentType?> GetByNameAsync(string name);
    }
}
