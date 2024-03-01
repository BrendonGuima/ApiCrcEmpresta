using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DefaultNamespace;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmprestimoController : Controller
{
    private readonly AppDbContext _context;

    public EmprestimoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> LendForId(int id)
    {
        var modelI = await _context.Items.FindAsync(id);
        if (modelI == null) NotFound();

        var item = new Emprestimo()
        {
            CategoryId = modelI.CategoryId,
            Name = modelI.Name,
            Code = modelI.Code,
            Date = DateTime.Now
        };
        _context.Emprestimo.Add(item);
        _context.Items.Remove(modelI);
        await _context.SaveChangesAsync();
        return Ok(modelI);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var model = await _context.Emprestimo.ToListAsync();
        return Ok(model);
    }
}