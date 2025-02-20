using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymFitness.Infrastructure.Data;
using GymFitness.Infrastructure.Repositories.Abstractions;

namespace GymFitness.Infrastructure.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly GymbotDbContext _context;

        public void AddStaff(Staff staff)
        {
            // Kiểm tra Staff đã tồn tại chưa
            Staff temp = _context.Staffs.FirstOrDefault(x => x.Email.ToLower() == staff.Email.ToLower());
            if (temp == null)
            {
                // 🔹 Tìm Role với tên "Staff" trong database
                var role = _context.Roles.FirstOrDefault(r => r.Name == "Staff");
                if (role == null)
                {
                    throw new Exception("Role 'Staff' không tồn tại trong hệ thống.");
                }
                // Gán Role cho Staff
                staff.RoleId = role.RoleId;  // Chỉ cần gán RoleId, không cần RoleName
                staff.PasswordHash = "123456";  // Mật khẩu mặc định
                // Thêm Staff vào database
                _context.Staffs.Add(staff);
                _context.SaveChanges();
            }
        }

        public async Task<Staff> GetStaffByEmail(string email)
        {
            return await _context.Staffs.Include(x => x.Role)
                                        .FirstOrDefaultAsync(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)); 
        }
    }
}
