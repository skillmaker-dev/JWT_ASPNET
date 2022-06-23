namespace JWT_PracticalDemo.Services
{
    public interface IAuthenticationService
    {
        public (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password);
    }
}
