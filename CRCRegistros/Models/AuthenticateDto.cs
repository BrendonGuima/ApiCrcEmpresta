using System.ComponentModel.DataAnnotations;

namespace CRCRegistros.Models;

public class AuthenticateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
}