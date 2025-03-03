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

        public async Task<IEnumerable<StaffSchedule>> GetAllSchedulesAsync() =>
            await _staffSchedule.GetAllAsync();

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
