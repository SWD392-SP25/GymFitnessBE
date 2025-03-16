using GymFitness.API.Services.Abstractions;
using GymFitness.Application.Abstractions.Services;
using GymFitness.Application.ResponseDto;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRedisService _redisService;
        public UserService(IUserRepository userRepository, IRedisService redisService)
        {
            _userRepository = userRepository;
            _redisService = redisService;
        }
        public async Task AddUser(User user)
        {
            await _userRepository.AddUser(user);

        }

        public async Task<bool> BanUser(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null || user.Status == "Banned")
            {
                return false;
            }

            // ✅ Revoke token sau khi ban user
            await _redisService.SetAsync($"revoke:{user.UserId}", "true", TimeSpan.FromHours(3));
            return await _userRepository.BanUser(email);
        }

        public async Task<List<UserResponseDto>> GetAll(string? filterOn, string? filterQuery)
        {
            var users = await _userRepository.GetUsersAsync(filterOn, filterQuery);

            // Kiểm tra nếu không có user nào, trả về danh sách rỗng (tránh null)
            if (users == null || !users.Any())
            {
                return new List<UserResponseDto>();
            }

            // Chuyển đổi danh sách user thành danh sách UserResponseDto
            List<UserResponseDto> userResponseDtos = users.Select(user => new UserResponseDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                ProfilePictureUrl = user.ProfilePictureUrl,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                State = user.State,
                PostalCode = user.PostalCode,
                Country = user.Country,
                EmergencyContactName = user.EmergencyContactName,
                EmergencyContactPhone = user.EmergencyContactPhone,
                Role = user.Role.Name,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                LastLogin = user.LastLogin
            }).ToList();

            return userResponseDtos;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
        }
    }
}
