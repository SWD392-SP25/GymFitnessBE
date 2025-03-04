using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IUserMeasurementRepository
    {
        Task<List<UserMeasurement>> GetUserMeasurementsAsync();

        Task<UserMeasurement?> GetUserMeasurementByIdAsync(Guid userId);

        Task AddAsync(UserMeasurement userMeasurement);

        Task UpdateAsync(UserMeasurement userMeasurement);

        Task DeleteAsync(Guid userId);
    }
}
