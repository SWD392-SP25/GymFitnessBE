
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Repositories
{
     public interface INotificationRepository
    {
       
        Task AddNotificationAsync(Notification notification);
        
        Task DeleteNotificationAsync(int input);

    }
}
