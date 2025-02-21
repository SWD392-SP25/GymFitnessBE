using GymFitness.Infrastructure.Data;

namespace GymFitness.API.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        void AddUser(User user);
    }
}
