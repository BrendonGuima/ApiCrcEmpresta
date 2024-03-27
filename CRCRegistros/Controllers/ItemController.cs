using System.Security.Cryptography;
using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CRCRegistros.Controllers;

[Authorize]
[Route("api/[Controller]")]
[ApiController]
public class ItemController : Controller
{
    private readonly MongoDbContext _context;
    
    public ItemController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateItemAndLinkToCategory(Item item)
    {
        await _context.Items.InsertOneAsync(item);

        var categoryFilter = Builders<Category>.Filter.Eq(c => c.Id, item.CategoryId);
        await _context.Category.FindOneAndUpdateAsync(
            categoryFilter,
            Builders<Category>.Update.Push(c => c.Items, item),
            new FindOneAndUpdateOptions<Category> { ReturnDocument = ReturnDocument.After }
        );

        return Ok(item);
    }
    
    [HttpPut("Edit/{id}")]
    public async Task<ActionResult> UpdateItemName(string id, [FromBody] Item newItem)
    {
        var item = await _context.Items.Find(i => i.Id == id).FirstOrDefaultAsync();
        if (item == null) return NotFound();

        item.Name = newItem.Name;
        await _context.Items.ReplaceOneAsync(i => i.Id == id, item);

        return NoContent();
    }
    
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var item = await _context.Items.FindOneAndDeleteAsync(x => x.Id == id);
        if (item == null) return NotFound();

        var updateDefinition = Builders<Category>.Update.PullFilter(c => c.Items, i => i.Id == id);
        var category = await _context.Category.FindOneAndUpdateAsync(
            Builders<Category>.Filter.Where(c => c.Items.Any(i => i.Id == id)),
            updateDefinition,
            new FindOneAndUpdateOptions<Category> { ReturnDocument = ReturnDocument.After });

        if (category == null) return NotFound();

        return NoContent();
    }
    
    [HttpGet]
    [Route("Get/{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var items = await _context.Items.Find(i => i.Id == id).FirstOrDefaultAsync();
        if (items == null)return NotFound();
        return Ok(items);
    }
    
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult> GetAll()
    {
        var model = await _context.Items.Find(_ => true).ToListAsync();
        return Ok(model);
    }
    
    
}