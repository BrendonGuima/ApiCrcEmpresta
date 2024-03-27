using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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

    [HttpPost("Create")]
    public async Task<ActionResult> Create(Category model)
    {
        await _context.Category.InsertOneAsync(model);
        return Ok(model);
    }

    [HttpPut("Edit/{id}")]
    public async Task<ActionResult> UpdateCategoryName(string id, [FromBody] Category newCategory)
    {
        var category = await _context.Category.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (category == null) return NotFound();
        category.Name = newCategory.Name;
        await _context.Category.ReplaceOneAsync(c => c.Id == id, category);
        return NoContent();
    }
    
    

    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _context.Items.DeleteManyAsync(x => x.CategoryId == id);

        var category = await _context.Category.FindOneAndDeleteAsync(x => x.Id == id);
        if (category == null) NotFound();
        return NoContent();
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var category = await _context.Category.Find(c => c.Id == id).FirstOrDefaultAsync();
        if (category == null) return NotFound();

        var items = await _context.Items.Find(i => i.CategoryId == id).ToListAsync();
        category.Items = items;

        return Ok(category);
    }
    
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        var model = await _context.Category.Find(_ => true).ToListAsync();
        return Ok(model);
    }

}