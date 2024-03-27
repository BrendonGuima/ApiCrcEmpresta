using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CRCRegistros.Models;

public class ItemLending
{

    public string Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string CategoryId { get; set; }

    public DateTime Date { get; set; }

    public string StudentName { get; set; }

    public string StudentId { get; set; }
    

}