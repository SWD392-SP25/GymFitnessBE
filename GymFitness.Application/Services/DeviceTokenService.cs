﻿using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class DeviceTokenService : IDeviceTokenService
    {
        private readonly IDeviceTokenRepository _deviceTokenRepository;

        public DeviceTokenService(IDeviceTokenRepository deviceTokenRepository)
        {
            _deviceTokenRepository = deviceTokenRepository;
        }

        public async Task<bool> AddDeviceTokenAsync(string deviceToken)
        {
            return await _deviceTokenRepository.AddDeviceTokenAsync(deviceToken);
        }

        public async Task<List<string>> GetDeviceTokensAsync()
        {
            var deviceTokens = await _deviceTokenRepository.GetDeviceTokensAsync();
            return new List<string>(deviceTokens.Select(x => x.DeviceToken1));
        }

     
    }
}
