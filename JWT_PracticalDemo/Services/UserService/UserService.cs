using System.Security.Claims;

namespace JWT_PracticalDemo.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public object GetCurrentUser()
        {
            var username = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var role = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            return new { username, role };
        }
    }
}
