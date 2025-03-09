using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class StaffSpecializationRepository : IStaffSpecializationRepository
    {
        private readonly GymbotDbContext _context;

        public StaffSpecializationRepository(GymbotDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StaffSpecialization>> GetAllAsync(string? filterOn, 
                                                                  string? filterQuery, 
                                                                  string? sortBy, 
                                                                  bool? isAscending, 
                                                                  int? pageNumber = 1, 
                                                                  int? pageSize = 10)
        {
            var query = _context.StaffSpecializations.Include(x => x.Staff).Include(x => x.Specialization).AsQueryable();
            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                query = filterOn switch
                {
                    "staff" => query.Where(x => x.Staff.Email.ToString() == filterQuery),
                    "name" => query.Where(x => x.Specialization.Name.ToString() == filterQuery),
                    _ => query
                };
            }
            // **Sắp xếp**
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                query = sortBy switch
                {
                    "staff" => isAscending == true ? query.OrderBy(x => x.Staff.Email) : query.OrderByDescending(x => x.Staff.Email),
                    "name" => isAscending == true ? query.OrderBy(x => x.Specialization.Name) : query.OrderByDescending(x => x.Specialization.Name),
                    _ => query
                };
            }
            // **Phân trang**
            return await query.Skip((pageNumber.Value - 1) * pageSize.Value)
                        .Take(pageSize.Value)
                        .ToListAsync();
        }

        public async Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId) =>
            await _context.StaffSpecializations
                .FirstOrDefaultAsync(s => s.StaffId == staffId && s.SpecializationId == specializationId);

        public async Task AddAsync(StaffSpecialization staffSpecialization)
        {
            _context.StaffSpecializations.Add(staffSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StaffSpecialization staffSpecialization)
        {
            _context.StaffSpecializations.Update(staffSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid staffId, int specializationId)
        {
            var staffSpecialization = await _context.StaffSpecializations
                .FirstOrDefaultAsync(s => s.StaffId == staffId && s.SpecializationId == specializationId);

            if (staffSpecialization != null)
            {
                _context.StaffSpecializations.Remove(staffSpecialization);
                await _context.SaveChangesAsync();
            }
        }

        
    }
}
