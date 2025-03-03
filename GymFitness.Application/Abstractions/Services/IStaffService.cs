using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetAllStaffsAsync();
        Task<Staff?> GetStaffByIdAsync(Guid id);
        Task<Staff?> GetByEmailAsync(string email);
        Task AddStaffAsync(Staff staff);
        Task UpdateStaffAsync(Staff staff);
        Task DeleteStaffAsync(Guid id);
    }
}
