using GymFitness.API.Services.Abstractions;
using GymFitness.Infrastructure.Data;
using GymFitness.Infrastructure.Repositories.Abstractions;

namespace GymFitness.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
 
        public async void AddUser(User user)
        {
             _userRepository.AddUser(user);
        }
    }
}
