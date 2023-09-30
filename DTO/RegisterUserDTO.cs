using System.ComponentModel.DataAnnotations;

namespace Scholarit.DTO
{
    public class RegisterUserDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
