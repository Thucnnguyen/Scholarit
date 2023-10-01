using System.ComponentModel.DataAnnotations;

namespace Scholarit.DTO
{
    public class RoleAddDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
