using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class UserMeasurementService : IUserMeasurementService
    {
        private readonly IUserMeasurementRepository _userMeasurement;

        public UserMeasurementService(IUserMeasurementRepository userMeasurement)
        {
            _userMeasurement = userMeasurement;
        }
        public async Task AddUserMeasurementAsync(UserMeasurement userMeasurement)
        {
            await _userMeasurement.AddAsync(userMeasurement);
        }

        public async Task DeleteUserMeasurementAsync(Guid userId)
        {
            await _userMeasurement.DeleteAsync(userId);
        }

        public async Task<UserMeasurement?> GetUserMeasurementByIdAsync(Guid userId)
        {
            return await _userMeasurement.GetUserMeasurementByIdAsync(userId);
        }

        public async Task<List<UserMeasurement>> GetUserMeasurementsAsync()
        {
            return await _userMeasurement.GetUserMeasurementsAsync();
        }

        public async Task UpdateUserMeasurementAsync(UserMeasurement userMeasurement)
        {
            await _userMeasurement.UpdateAsync(userMeasurement);
        }
    }
}
