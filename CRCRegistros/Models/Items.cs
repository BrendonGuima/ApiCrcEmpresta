using System.ComponentModel.DataAnnotations;

namespace CRCRegistros.Models;

public class Items
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public int CategoryId { get; set; }
}

