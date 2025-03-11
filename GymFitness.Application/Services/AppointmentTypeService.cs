﻿using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class AppointmentTypeService : IAppointmentTypeService
    {
        private readonly IAppointmentTypeRepository _appointmentType;

        public AppointmentTypeService(IAppointmentTypeRepository appointmentType)
        {
            _appointmentType = appointmentType;
        }

        public async Task<IEnumerable<AppointmentTypeResponseDto>> GetAllAppointmentTypesAsync(string? filterOn, string? filterQuery, int pageNumber, int pageSize)
        {
            var appointmentType = await _appointmentType.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);
            return appointmentType.Select(a => new AppointmentTypeResponseDto 
            {
               
                Description = a.Description,
                DurationMinutes = a.DurationMinutes,
                Price = a.Price
            }).ToList();
        }
            

        public async Task<AppointmentType?> GetAppointmentTypeByIdAsync(int typeId) =>
            await _appointmentType.GetByIdAsync(typeId);

        public async Task AddAppointmentTypeAsync(AppointmentType appointmentType) =>
            await _appointmentType.AddAsync(appointmentType);

        public async Task UpdateAppointmentTypeAsync(AppointmentType appointmentType) =>
            await _appointmentType.UpdateAsync(appointmentType);

        public async Task DeleteAppointmentTypeAsync(int typeId) =>
            await _appointmentType.DeleteAsync(typeId);


    }
}
