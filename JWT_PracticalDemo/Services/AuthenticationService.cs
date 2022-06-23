using System.Security.Cryptography;

namespace JWT_PracticalDemo.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }  
        }
    }
}
