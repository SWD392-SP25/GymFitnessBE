using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class DeviceTokenRepository : IDeviceTokenRepository
    {
        private readonly GymbotDbContext _repository;

        public DeviceTokenRepository(GymbotDbContext repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddDeviceTokenAsync(string deviceToken)
        {
            List<DeviceToken> deviceTokens = await _repository.DeviceTokens.ToListAsync();

            if (deviceTokens.Any(x => x.DeviceToken1 == deviceToken))
            {
                return false;
            }
            else
            {
                await _repository.DeviceTokens.AddAsync(new DeviceToken {Id = Guid.NewGuid(), DeviceToken1 = deviceToken });
                await _repository.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<DeviceToken>> GetDeviceTokensAsync()
        {
            return await _repository.DeviceTokens.ToListAsync();
        }
    }
}
