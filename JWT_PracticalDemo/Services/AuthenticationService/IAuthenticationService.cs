using JWT_PracticalDemo.Models;

namespace JWT_PracticalDemo.Services
{
    public interface IAuthenticationService
    {
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
        public bool VerifyPasswordHash(string password,byte[] passwordSalt, byte[] passwordHash);
        public string GenerateToken(User user);
        public RefreshToken GenerateRefreshToken();
        public void SetRefreshToken(RefreshToken refreshToken, User user);
    }
}
