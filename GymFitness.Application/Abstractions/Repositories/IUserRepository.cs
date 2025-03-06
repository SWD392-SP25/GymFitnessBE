using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);

        Task<User> GetUserById(Guid userId);
        Task<List<User>> GetUsersAsync(string? filterOn, string? filterQuery);
        Task UpdateUser(User user);
        Task<bool> BanUser(Guid userId);
    }
}
