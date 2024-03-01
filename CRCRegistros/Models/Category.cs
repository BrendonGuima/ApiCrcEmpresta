using System.ComponentModel.DataAnnotations;

namespace CRCRegistros.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    
    public ICollection<Items> Item { get; set; }
}