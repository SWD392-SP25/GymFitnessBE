using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staff;

        public StaffService(IStaffRepository staff)
        {
            _staff = staff;
        }

        public async Task<IEnumerable<StaffResponseDto>> GetAllStaffsAsync(string? filterOn,
                                                                           string? filterQuery,
                                                                           string? sortBy,
                                                                           bool? isAscending,
                                                                           int pageNumber = 1,
                                                                           int pageSize = 10)
        {
            var staffs = await _staff.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return staffs.Select(a => new StaffResponseDto 
            {
                StaffId = a.StaffId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email,
                Role = a.Role.Name ?? "No Role",
                CreatedAt = a.CreatedAt,
                Department = a.Department,
                HireDate = a.HireDate,
                LastLogin = a.LastLogin,
                Phone = a.Phone,
                Salary = a.Salary,
                Status = a.Status,
                SupervisorId = a.SupervisorId,
                TerminationDate = a.TerminationDate
            }).ToList();
        }


        public async Task<Staff?> GetStaffByIdAsync(Guid id) =>
            await _staff.GetByIdAsync(id);

        public async Task AddStaffAsync(Staff staff) =>
            await _staff.AddAsync(staff);

        public async Task UpdateStaffAsync(Staff staff) =>
            await _staff.UpdateAsync(staff);

        public async Task DeleteStaffAsync(Guid id) =>
            await _staff.DeleteAsync(id);

        public async Task<Staff?> GetByEmailAsync(string email) =>
            await _staff.GetByEmailAsync(email);

        public async Task<StaffResponseDto?> GetStaffForChat(string email)
        {
            var staff = await _staff.GetByEmailAsync(email);
            if (staff == null) return null;
            return new StaffResponseDto
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                Role = staff.Role.Name ?? "No Role",
                CreatedAt = staff.CreatedAt,
                Department = staff.Department,
                HireDate = staff.HireDate,
                LastLogin = staff.LastLogin,
                Phone = staff.Phone,
                Salary = staff.Salary,
                Status = staff.Status,
                SupervisorId = staff.SupervisorId,
                TerminationDate = staff.TerminationDate
            };
        }
    }
}
