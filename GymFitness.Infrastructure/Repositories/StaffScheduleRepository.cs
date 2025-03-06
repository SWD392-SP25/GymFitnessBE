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

        public Task<IEnumerable<StaffSchedule>> GetAllAsync(string? filterOn, 
                                                            string? filterQuery, 
                                                            int pageNumber = 1, 
                                                            int pageSize = 10)
        {
            var staffSchedules = _context.StaffSchedules.Include(x => x.Staff).AsQueryable();
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();

                staffSchedules = filterOn switch
                {
                    "email" => staffSchedules.Where(s => EF.Functions.Like(s.Staff.Email, $"%{filterQuery}%")),

                    "available" => bool.TryParse(filterQuery, out bool isAvailable)
                        ? staffSchedules.Where(s => s.IsAvailable == isAvailable)
                        : staffSchedules,

                    "location" => staffSchedules.Where(s => EF.Functions.Like(s.Location.ToString(), $"%{filterQuery}%")),

                   

                    _ => staffSchedules
                };
            }

            throw new NotImplementedException();
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
