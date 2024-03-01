using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CRCRegistros.Models;

public class Emprestimo
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    

}