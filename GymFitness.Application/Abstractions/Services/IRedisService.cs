using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface IRedisService
    {
        Task SetAsync(string key, string value, TimeSpan expiry);
        Task<string?> GetAsync(string key);
        Task<bool> DeleteAsync(string key);
    }
}
