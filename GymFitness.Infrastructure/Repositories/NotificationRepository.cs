﻿using GymFitness.Application.Abstractions.Repositories;
using GymFitness.Domain.Entities;
using GymFitness.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly GymbotDbContext _context;

        public NotificationRepository(GymbotDbContext context)
        {
            _context = context;
        }
        public async Task AddNotificationAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int input)
        {
            var entity = _context.Notifications.Find(input);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }
            _context.Notifications.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
