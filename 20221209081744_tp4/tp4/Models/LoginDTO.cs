using System.ComponentModel.DataAnnotations;

namespace tp3.Models
{
    public class LoginDTO
    {
        [Required]  
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
