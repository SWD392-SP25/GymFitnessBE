using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffScheduleService
    {
        private readonly IStaffScheduleRepository _repository;

        public StaffScheduleService(IStaffScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StaffSchedule>> GetAllSchedulesAsync() =>
            await _repository.GetAllAsync();

        public async Task<StaffSchedule?> GetScheduleByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddScheduleAsync(StaffSchedule schedule) =>
            await _repository.AddAsync(schedule);

        public async Task UpdateScheduleAsync(StaffSchedule schedule) =>
            await _repository.UpdateAsync(schedule);

        public async Task DeleteScheduleAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
