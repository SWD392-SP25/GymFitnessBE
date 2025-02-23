using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class StaffScheduleRepository : IStaffScheduleRepository
    {
        private readonly GymbotDbContext _context;

        public StaffScheduleRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffSchedule>> GetAllAsync() =>
            await _context.StaffSchedules.ToListAsync();

        public async Task<StaffSchedule?> GetByIdAsync(int id) =>
            await _context.StaffSchedules.FindAsync(id);

        public async Task AddAsync(StaffSchedule schedule)
        {
            _context.StaffSchedules.Add(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StaffSchedule schedule)
        {
            _context.StaffSchedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await _context.StaffSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.StaffSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
