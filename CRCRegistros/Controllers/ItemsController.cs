using System.Security.Cryptography;
using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRCRegistros.Controllers;

[Authorize]
[Route("api/[Controller]")]
[ApiController]
public class ItemsController : Controller
{
    private readonly AppDbContext _context;

    public ItemsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Items model)
    {
        _context.Items.Add(model);
        await _context.SaveChangesAsync();
        return Ok(model);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var model = await _context.Items.ToListAsync();
        return Ok(model);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var model = await _context.Items.FindAsync(id);
        if (model == null) NotFound();
        _context.Items.Remove(model);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}