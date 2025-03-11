using FirebaseAdmin.Auth;
using GymFitness.Domain.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymFitness.Domain.Services
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        public async Task<string> GenerateFirebaseToken(string uid)
        {
            try
            {
                var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
                return customToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating token: {ex.Message}");
                throw;
            }
        }

        public async Task<FirebaseToken> VerifyIdToken(string idToken)
        {
            try
            {
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                return decodedToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid Token: {ex.Message}");
                throw;
            }
        }
    }
}
