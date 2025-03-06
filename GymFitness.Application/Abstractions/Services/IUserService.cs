using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;

namespace GymFitness.API.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);

        Task<User> GetUserById(Guid userId);
        Task<List<UserResponseDto>> GetAll(string? filterOn, string? filterQuery);
        Task UpdateUser(User user);
        Task<bool> BanUser(Guid userId);
    }
}
