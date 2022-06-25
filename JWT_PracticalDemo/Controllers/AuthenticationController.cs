using JWT_PracticalDemo.DTO;
using JWT_PracticalDemo.Models;
using JWT_PracticalDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_PracticalDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        public static User user { get; set; } = new();
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// Api route to register a user using a username and a password
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Created user if success</returns>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO userDTO)
        {
            //using tuples to get our password hash and password salt from the method and returning the user.
            (var passwordHash, var passwordSalt) = authenticationService.CreatePasswordHash(userDTO.Password);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = userDTO.Username;

            return Ok(user);
        }

        /// <summary>
        /// Api route to login using username and password
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>string of the generated token</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDTO userDTO)
        {
            if (userDTO.Username != user.Username)
                return NotFound("User doesn't exist");

            if (!authenticationService.VerifyPasswordHash(userDTO.Password, user.PasswordSalt, user.PasswordHash))
                return BadRequest("Wrong password");


            return Ok(authenticationService.GenerateToken(user));
        }
    }
}
