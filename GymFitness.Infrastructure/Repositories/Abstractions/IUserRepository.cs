﻿using GymFitness.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Infrastructure.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        void AddUser(User user);

    }
}
