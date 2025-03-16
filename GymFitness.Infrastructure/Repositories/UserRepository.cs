using GymFitness.Infrastructure.Data;
using GymFitness.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using GymFitness.Domain.Entities;


namespace GymFitness.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GymbotDbContext _context;

        public UserRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public Task<List<User>> GetUsersAsync(string? filterOn, string? filterQuery)
        {
            var users = _context.Users
                .Include(u => u.Role)
                .AsQueryable();

            // **Lọc theo filterOn và filterQuery**
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.ToLower();
                filterQuery = filterQuery.ToLower();
                users = filterOn switch
                {
                    "email" => users.Where(u => u.Email != null && u.Email.ToLower().Contains(filterQuery)),
                    "phone" => users.Where(u => u.Phone != null && u.Phone.ToLower().Contains(filterQuery)),
                    "lastname" => users.Where(u => u.LastName != null && u.LastName.ToLower().Contains(filterQuery)),
                    "gender" => users.Where(u => u.Gender != null && u.Gender.ToLower().Contains(filterQuery)),
                    "city" => users.Where(u => u.City != null && u.City.ToLower().Contains(filterQuery)),
                    "status" => users.Where(u => u.Status != null && u.Status.ToLower().Contains(filterQuery)),
                    
                    _ => users
                };
            }
            return users.ToListAsync();
        }
        public async Task AddUser(User user)
        {
            // Kiểm tra User đã tồn tại chưa
            User temp = await _context.Users
                                      .FirstOrDefaultAsync(x => EF.Functions.Like(x.Email, user.Email));

            if (temp == null)
            {
                // 🔹 Tìm Role với tên "User" trong database
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");

                if (role == null)
                {
                    throw new Exception("Role 'User' không tồn tại trong hệ thống.");
                }

                // Gán Role cho User
                user.RoleId = role.RoleId;

                // Thêm User vào database
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<User> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(email));
            }

            Console.WriteLine($"GetUserByEmail called with email: {email}");

            var returnUser = _context.Users.Include(x => x.Role)
                         .FirstOrDefaultAsync(x => EF.Functions.Like(x.Email, email));
            return await returnUser;

            Console.WriteLine("UserRepository found with role: ", returnUser.Result.Role);


        }

        public async Task<User> GetUserById(Guid userId)
        {
            return await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userId);

        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await GetUserById(user.UserId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Phone = user.Phone;
            existingUser.DateOfBirth = user.DateOfBirth;
            existingUser.ProfilePictureUrl = user.ProfilePictureUrl;
            existingUser.AddressLine1 = user.AddressLine1;
            existingUser.AddressLine2 = user.AddressLine2;
            existingUser.City = user.City;
            existingUser.State = user.State;
            existingUser.PostalCode = user.PostalCode;
            existingUser.Country = user.Country;
            existingUser.EmergencyContactName = user.EmergencyContactName;
            existingUser.EmergencyContactPhone = user.EmergencyContactPhone;
            
        }

        public async Task<bool> BanUser(string email)
        {
            var user = await GetUserByEmail(email);
         
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.Status = "Banned";
            await _context.SaveChangesAsync();
            return true;
            
        }
    }
}
