using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZstdSharp.Unsafe;

namespace CRCRegistros.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly MongoDbContext _context;

   public CategoryController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult> Create(Category model)
    {
     await _context.CreateCategory(model);
        return Ok(model);
    }

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
    
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        var model = await _context.GetAllCategorys();
        return Ok(model);
    }

}