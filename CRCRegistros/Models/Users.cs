using System.ComponentModel.DataAnnotations;

namespace CRCRegistros.Models;

public class Users
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public Perfil Perfil { get; set; }
}

public enum Perfil
{
    Administrador,
    Usuario
}