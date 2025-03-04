
using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Application.Abstractions.Services
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(Notification notification, string deviceToken);
    }
}
