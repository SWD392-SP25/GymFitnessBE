using GymFitness.API.Services.Abstractions;
using GymFitness.Domain.Entities;
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
            var returnUser = _userRepository.GetUserByEmail(email);
            return await returnUser;
            Console.WriteLine("UserService found with role: ", returnUser.Result.Role);
        }
 
        public async void AddUser(User user)
        {
             _userRepository.AddUser(user);
        }

        public Task<User> GetUserById(Guid userId)
        {
           return _userRepository.GetUserById(userId);
        }
    }
}
