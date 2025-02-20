using GymFitness.Infrastructure.Data;
using GymFitness.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace GymFitness.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GymbotDbContext _context;
        public async void AddUser(User user)
        {
            // Kiểm tra User đã tồn tại chưa
            User temp = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == user.Email.ToLower());

            if (temp == null)
            {
                // 🔹 Tìm Role với tên "User" trong database
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");

                if (role == null)
                {
                    throw new Exception("Role 'User' không tồn tại trong hệ thống.");
                }

                // Gán Role cho User
                user.RoleId = role.RoleId;  // Chỉ cần gán RoleId, không cần RoleName
                user.PasswordHash = "123456";  // Mật khẩu mặc định
                // Thêm User vào database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
