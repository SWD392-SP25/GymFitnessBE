using GymFitness.Domain.Entities;

namespace GymFitness.API.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        void AddUser(User user);

        Task<User> GetUserById(Guid userId);
    }
}
