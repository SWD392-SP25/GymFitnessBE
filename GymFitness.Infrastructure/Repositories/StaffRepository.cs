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

        public async Task<IEnumerable<Staff>> GetAllAsync(string? filterOn = null,
                                                          string? filterQuery = null,
                                                          string? sortBy = null,
                                                          bool? isAscending = true,
                                                          int pageNumber = 1,
                                                          int pageSize = 10)
        {
            var query = _context.Staffs.Include(x => x.Role).AsQueryable();
            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                query = filterOn switch
                {
                    "name" => query.Where(x => EF.Functions.Like(x.LastName, filterQuery)),
                    "email" => query.Where(x => EF.Functions.Like(x.Email, filterQuery)),
                    _ => query
                };
            }
            // **Sắp xếp**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy switch
                {
                    "salary" => isAscending == true ? query.OrderBy(x => x.Salary) : query.OrderByDescending(x => x.Salary),
                    
                    _ => query
                };
            }
            // **Phân trang**
            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }

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
