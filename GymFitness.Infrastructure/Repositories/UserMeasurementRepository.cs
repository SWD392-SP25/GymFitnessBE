using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public class UserMeasurementRepository : IUserMeasurementRepository
    {
        private readonly GymbotDbContext _context;

        public UserMeasurementRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserMeasurement userMeasurement)
        {
            var existingUserMeasurement = await _context.UserMeasurements.FirstOrDefaultAsync(x => x.UserId == userMeasurement.UserId);

            if (existingUserMeasurement == null)
            {
                _context.UserMeasurements.Add(userMeasurement);
                await _context.SaveChangesAsync();
                return;
            }
            await UpdateAsync(userMeasurement);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var existingUserMeasurement = await _context.UserMeasurements.FirstOrDefaultAsync(x => x.UserId == userId);
            if (existingUserMeasurement != null)
            {
                _context.UserMeasurements.Remove(existingUserMeasurement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserMeasurement?> GetUserMeasurementByIdAsync(Guid userId)
        {
            return await _context.UserMeasurements.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<List<UserMeasurement>> GetUserMeasurementsAsync()
        {
            return await _context.UserMeasurements.ToListAsync();
        }

        public async Task UpdateAsync(UserMeasurement userMeasurement)
        {
            var existingUserMeasurement =  await _context.UserMeasurements.FirstOrDefaultAsync(x => x.UserId == userMeasurement.UserId);
            if (existingUserMeasurement != null) 
            {
                existingUserMeasurement.Date = userMeasurement.Date;
                existingUserMeasurement.Weight = userMeasurement.Weight;
                existingUserMeasurement.Height = userMeasurement.Height;
                existingUserMeasurement.BodyFatPercentage = userMeasurement.BodyFatPercentage;
                existingUserMeasurement.ChestCm = userMeasurement.ChestCm;
                existingUserMeasurement.WaistCm = userMeasurement.WaistCm;
                existingUserMeasurement.HipsCm = userMeasurement.HipsCm;
                existingUserMeasurement.ArmsCm = userMeasurement.ArmsCm;
                existingUserMeasurement.ThighsCm = userMeasurement.ThighsCm;
                existingUserMeasurement.Notes = userMeasurement.Notes;
                existingUserMeasurement.CreatedAt = userMeasurement.CreatedAt;
                await _context.SaveChangesAsync();
            }
        }
    }
}
