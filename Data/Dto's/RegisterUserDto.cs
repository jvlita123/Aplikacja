using System.ComponentModel.DataAnnotations;

namespace Data.Dto_s
{
    public class RegisterUserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; } = 1;
    }
}
