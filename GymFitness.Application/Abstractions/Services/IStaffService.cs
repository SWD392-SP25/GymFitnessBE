using GymFitness.Application.ResponseDto;
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
        Task<IEnumerable<StaffResponseDto>> GetAllStaffsAsync(string? filterOn,
                                                              string? filterQuery,
                                                              string? sortBy,
                                                              bool? isAscending,
                                                              int pageNumber = 1,
                                                              int pageSize = 10);
        Task<Staff?> GetStaffByIdAsync(Guid id);
        Task<Staff?> GetByEmailAsync(string email);
        Task AddStaffAsync(Staff staff);
        Task UpdateStaffAsync(Staff staff);
        Task DeleteStaffAsync(Guid id);
        Task<StaffResponseDto?> GetStaffForChat(string email);
    }
}
