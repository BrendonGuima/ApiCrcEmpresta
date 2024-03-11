using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRCRegistros.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
//    private readonly AppDbContext _context;

//    public CategoryController(AppDbContext context)
 //   {
//        _context = context;
//    }

//    [HttpPost]
//    public async Task<ActionResult> Create(Category model)
 //   {
//        _context.Category.Add(model);
 //       await _context.SaveChangesAsync();
 //       return Ok(model);
//    }

//    [HttpDelete("{id}")]
 //   public async Task<ActionResult> Delete(int id)
 //   {
//        var model = await _context.Category.FindAsync(id);
 //       if (model == null) NotFound();
 //       _context.Category.Remove(model);
 //       await _context.SaveChangesAsync();
//        return NoContent();
 //   }

 //   [HttpGet("{id}")]
//    public async Task<ActionResult> GetById(int id)
 //   {
 //       var model = await _context.Category
 //           .Include(d => d.Item)
  //          .FirstOrDefaultAsync(c => c.Id == id);
//        if (model == null) NotFound();
 //       return Ok(model);
//    }
    
 //   [HttpGet]
 //   public async Task<ActionResult> GetAll()
 //   {
//        var model = await _context.Category.Include(d => d.Item).ToListAsync();
 //       return Ok(model);
 //   }

}