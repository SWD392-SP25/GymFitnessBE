using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly GymbotDbContext _context;

        public StaffRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Staff>> GetAllAsync() =>
            await _context.Staffs.ToListAsync();

        public async Task<Staff?> GetByIdAsync(Guid id) =>
            await _context.Staffs.FindAsync(id);

        public async Task AddAsync(Staff staff)
        {
            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff != null)
            {
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Staff?> GetByEmailAsync(string email)
        {
            return await _context.Staffs.FirstOrDefaultAsync(x => EF.Functions.Like(x.Email, email));
        }
    }
}
