using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
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

        public async Task<IEnumerable<AppointmentType>> GetAllAppointmentTypesAsync(string? filterOn, string? filterQuery, int pageNumber, int pageSize) =>
            await _appointmentType.GetAllAsync(filterOn, filterQuery, pageNumber, pageSize);

        public async Task<AppointmentType?> GetAppointmentTypeByIdAsync(int typeId) =>
            await _appointmentType.GetByIdAsync(typeId);

        public async Task AddAppointmentTypeAsync(AppointmentType appointmentType) =>
            await _appointmentType.AddAsync(appointmentType);

        public async Task UpdateAppointmentTypeAsync(AppointmentType appointmentType) =>
            await _appointmentType.UpdateAsync(appointmentType);

        public async Task DeleteAppointmentTypeAsync(int typeId) =>
            await _appointmentType.DeleteAsync(typeId);

        public async Task<AppointmentType?> GetAppointmentTypeByNameAsync(string name)
        {
            return await _appointmentType.GetByNameAsync(name);
        }
    }
}
