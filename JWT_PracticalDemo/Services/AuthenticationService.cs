using JWT_PracticalDemo.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JWT_PracticalDemo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// Returns hashcode of a given password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Tuple(byte[] passwordHash, byte[] passwordSalt)</returns>
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }  
        }


        /// <summary>
        /// Generate JWT token of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>string representing the generated token</returns>
        public string GenerateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            //generating the secret key using appsettings's key
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value));

            //creating credentials using the previous key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Verifies if the password hash matches the computed hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="passwordHash"></param>
        /// <returns>Boolean determining if the password is correct or not</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
           using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
