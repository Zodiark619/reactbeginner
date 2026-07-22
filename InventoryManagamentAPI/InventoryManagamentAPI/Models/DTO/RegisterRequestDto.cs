using System.ComponentModel.DataAnnotations;

namespace WebAPIBeginnerHerryWijaya.Models.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]

        public string Password { get; set; } = string.Empty;
    }
}
