using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IStaffScheduleService
    {
        Task<IEnumerable<StaffSchedule>> GetAllSchedulesAsync(string? filterOn,
                                                              string? filterQuery,
                                                              int pageNumber = 1,
                                                              int pageSize = 10);
        Task<StaffSchedule?> GetScheduleByIdAsync(int id);
        Task AddScheduleAsync(StaffSchedule schedule);
        Task UpdateScheduleAsync(StaffSchedule schedule);
        Task DeleteScheduleAsync(int id);
    }
}
