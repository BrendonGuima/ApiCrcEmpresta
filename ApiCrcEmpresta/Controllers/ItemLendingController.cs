using Amazon.Runtime.Internal;
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
      DateLend = DateTime.Now,
      DateReturn = null,
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
         DateLend = DateTime.Now,
         DateReturn = null,
         StudentName = itemLending.StudentName,
         StudentId = itemLending.StudentId
     };
     
     await _context.HistoryLendItems.InsertOneAsync(historyLending);

     return NoContent();
    }
    
    [HttpPost("Return/{id}")]
    public async Task<ActionResult> ReturnItem(string id)
    {
        var itemLending = await _context.Items.FindOneAndUpdateAsync(
            Builders<Item>.Filter.Eq(i => i.Id, id),
            Builders<Item>.Update.Set(i => i.IsLend, false)
                      .Set(i => i.LendeeName, null)
                      .Set(i => i.LendeeId, null)
        );
        if (itemLending == null) NotFound();
        //History
        var filter = Builders<History>.Filter.Eq(h => h.ItemId, id);
        var sort = Builders<History> .Sort.Descending(h => h.DateLend);
        var latestHistory = await _context.HistoryLendItems.Find(filter).Sort(sort).FirstOrDefaultAsync();

        if (latestHistory == null)
        {
            return NotFound(); // Nenhum histórico encontrado para atualizar
        }

        var update = Builders<History>.Update.Set(h => h.DateReturn, DateTime.Now);


        var updateResult = await _context.HistoryLendItems.UpdateOneAsync(
     Builders<History>.Filter.Eq(h => h.Id, latestHistory.Id),
     update
 );

        if (updateResult.ModifiedCount == 0)
        {
            return NotFound(); // Pode ser útil lidar com o caso em que não há histórico para atualizar
        }
        await _context.ItemLending.FindOneAndDeleteAsync(x => x.Id == id);
        return NoContent();
    }


    [HttpGet("History")]
    public async Task<ActionResult> GetHistory()
    {
        var items = await _context.HistoryLendItems.Find(_ => true).ToListAsync();
        return Ok(items);
    }
}