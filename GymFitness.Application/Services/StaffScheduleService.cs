using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffScheduleService : IStaffScheduleService
    {
        private readonly IStaffScheduleRepository _staffSchedule;

        public StaffScheduleService(IStaffScheduleRepository staffSchedule)
        {
            _staffSchedule = staffSchedule;
        }

        public async Task<IEnumerable<StaffSchedule>> GetAllSchedulesAsync(string? filterOn,
                                                                           string? filterQuery,
                                                                           int pageNumber = 1,
                                                                           int pageSize = 10)
        {
            return await _staffSchedule.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);
        }

        public async Task<StaffSchedule?> GetScheduleByIdAsync(int id) =>
            await _staffSchedule.GetByIdAsync(id);

        public async Task AddScheduleAsync(StaffSchedule schedule) =>
            await _staffSchedule.AddAsync(schedule);

        public async Task UpdateScheduleAsync(StaffSchedule schedule) =>
            await _staffSchedule.UpdateAsync(schedule);

        public async Task DeleteScheduleAsync(int id) =>
            await _staffSchedule.DeleteAsync(id);
    }
}
