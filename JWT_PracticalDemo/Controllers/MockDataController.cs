using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_PracticalDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MockDataController : ControllerBase
    {

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(new
            {
                Username = "anaschahid",
                FirstName = "Anas",
                LastName = "Chahid"
            }
            );
        }

    }
}
