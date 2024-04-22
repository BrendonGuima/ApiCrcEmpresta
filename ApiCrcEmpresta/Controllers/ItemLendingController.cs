using ApiCrcEmpresta;
using ApiCrcEmpresta.Models;
using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DefaultNamespace;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ItemLendingController : Controller
{
    private readonly MongoDbContext _context;

    public ItemLendingController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpPost("Lend/{id}")]
    public async Task<ActionResult> LendForId(string id, [FromBody] ItemLending request)
    {
     var item = await _context.Items.FindOneAndUpdateAsync(
      Builders<Item>.Filter.Eq(i => i.Id, id),
      Builders<Item>.Update
          .Set(i => i.IsLend, true)
          .Set(i => i.LendeeName, request.StudentName)
          .Set(i => i.LendeeId, request.StudentId)
     );

     if (item == null) return NotFound();

     var itemLending = new ItemLending()
     {
      Id   = item.Id,
      CategoryId = item.CategoryId,
      Name = item.Name,
      Code = item.Code,
      Date = DateTime.Now,
      StudentName = request.StudentName,
      StudentId = request.StudentId
     };
     await _context.ItemLending.InsertOneAsync(itemLending);
     
     //History
     var historyLending = new History()
     {
         ItemId = item.Id,
         CategoryId = item.CategoryId,
         Name = item.Name,
         Code = item.Code,
         Date = DateTime.Now,
         StudentName = itemLending.StudentName,
         StudentId = itemLending.StudentId
     };
     
     await _context.HistoryLendItems.InsertOneAsync(historyLending);

     return Ok(item);
    }
    
    [HttpPost("Return/{id}")]
    public async Task<ActionResult> ReturnItem(string id)
    {
        var itemLending = await _context.Items.FindOneAndUpdateAsync(
            Builders<Item>.Filter.Eq(i => i.Id, id),
            Builders<Item>.Update.Set(i => i.IsLend, false)
        );
        if (itemLending == null) NotFound();
        //History
        var historyLending = new History()
        {
            ItemId = itemLending.Id,
            CategoryId = itemLending.CategoryId,
            Name = itemLending.Name,
            Code = itemLending.Code,
            Date = DateTime.Now
            
        };
        await _context.HistoryLendItems.InsertOneAsync(historyLending);
        await _context.ItemLending.FindOneAndDeleteAsync(x => x.Id == id);
        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var model = await _context.ItemLending.Find(_ => true).ToListAsync();
        return Ok(model);
    }

    [HttpGet("History")]
    public async Task<ActionResult> GetHistory()
    {
        var items = await _context.HistoryLendItems.Find(_ => true).ToListAsync();
        return Ok(items);
    }
}