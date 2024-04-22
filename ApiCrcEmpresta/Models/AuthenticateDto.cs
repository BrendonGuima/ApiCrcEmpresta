using System.ComponentModel.DataAnnotations;

namespace ApiCrcEmpresta.Models
{
    public class AuthenticateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
