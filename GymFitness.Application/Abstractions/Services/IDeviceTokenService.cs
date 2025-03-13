using GymFitness.Application.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IDeviceTokenService
    {
        Task<bool> AddDeviceTokenAsync(string deviceToken);
        Task<List<string>> GetDeviceTokensAsync();
    }
}
