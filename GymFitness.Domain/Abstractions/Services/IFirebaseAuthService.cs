using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace GymFitness.Domain.Abstractions.Services
{
    public interface IFirebaseAuthService
    {
        Task<string> GenerateFirebaseToken(string uid);
        Task<FirebaseToken> VerifyIdToken(string idToken);
    }
}
