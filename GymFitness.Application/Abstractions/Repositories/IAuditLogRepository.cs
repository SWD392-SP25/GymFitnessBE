using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
    public interface IAuditLogRepository
    {
        Task AddAsync(string action, string tableName, string recordId, string record, string userId);

        Task<List<AuditLog>> GetAuditLogsAsync();

        Task<List<AuditLog>> GetAuditLogsByTableNameAsync(string tableName);

        Task<List<AuditLog>> GetAuditLogsByUserIdAsync(string userId);

        



    }
}
