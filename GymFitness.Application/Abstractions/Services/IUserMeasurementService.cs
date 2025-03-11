using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IUserMeasurementService
    {
        Task AddUserMeasurementAsync(UserMeasurement userMeasurement);
        Task UpdateUserMeasurementAsync(UserMeasurement userMeasurement);
        Task DeleteUserMeasurementAsync(Guid userId);
        Task<List<UserMeasurement>> GetUserMeasurementsAsync();
        Task<UserMeasurement?> GetUserMeasurementByIdAsync(Guid userId);
    }
}
