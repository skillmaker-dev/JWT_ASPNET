using System.ComponentModel.DataAnnotations;

namespace JWT_PracticalDemo.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required"),MinLength(8,ErrorMessage = "Password must have at least 8 characters")]
        public string Password { get; set; } = string.Empty;
     }
}
