using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class StaffSpecializationService : IStaffSpecializationService
    {
        private readonly IStaffSpecializationRepository _staffSpecialization;

        public StaffSpecializationService(IStaffSpecializationRepository staffSpecialization)
        {
            _staffSpecialization = staffSpecialization;
        }

       
        public async Task<IEnumerable<StaffSpecializationResponseDto>> GetAllAsync(string? filterOn,
                                                                        string? filterQuery,
                                                                        string? sortBy,
                                                                        bool? isAscending,
                                                                        int pageNumber = 1,
                                                                        int pageSize = 10)
        {
            var staffSpecializations = await _staffSpecialization.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return staffSpecializations.Select(a => new StaffSpecializationResponseDto
            {
                StaffEmail = a.Staff.Email,
                SpecializationName = a.Specialization.Name != null ? a.Specialization.Name : null,
                CertificationNumber = a.CertificationNumber != null ? a.CertificationNumber : null,
                CertificationDate = a.CertificationDate != null ? a.CertificationDate : null,
                ExpirityDate = a.ExpiryDate != null ? a.ExpiryDate : null,
                Description = a.Specialization.Description != null ? a.Specialization.Description : null

            }).ToList();
        }
            

        public async Task<StaffSpecialization?> GetByIdAsync(Guid staffId, int specializationId) =>
            await _staffSpecialization.GetByIdAsync(staffId, specializationId);

        public async Task AddAsync(StaffSpecialization staffSpecialization) =>
            await _staffSpecialization.AddAsync(staffSpecialization);

        public async Task UpdateAsync(StaffSpecialization staffSpecialization) =>
            await _staffSpecialization.UpdateAsync(staffSpecialization);

        public async Task DeleteAsync(Guid staffId, int specializationId) =>
            await _staffSpecialization.DeleteAsync(staffId, specializationId);

        
    }
}
