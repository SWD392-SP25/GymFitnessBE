using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IStaffScheduleRepository
    {
        Task<IEnumerable<StaffSchedule>> GetAllAsync();
        Task<StaffSchedule?> GetByIdAsync(int id);
        Task AddAsync(StaffSchedule schedule);
        Task UpdateAsync(StaffSchedule schedule);
        Task DeleteAsync(int id);
    }
}
