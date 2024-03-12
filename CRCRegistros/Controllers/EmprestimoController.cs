using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefaultNamespace;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EmprestimoController : Controller
{
    private readonly MongoDbContext _context;

    public EmprestimoController(MongoDbContext context)
    {
        _context = context;
    }

//   [HttpPost("{id} (Emprestar)")]
//   public async Task<ActionResult> LendForId(int id)
  //  {
  //      var modelI = await _context.Items.FindAsync(id);
  //      if (modelI == null) NotFound();

 //       var item = new Emprestimo()
 //       {
 //           CategoryId = modelI.CategoryId,
 //           Name = modelI.Name,
 //           Code = modelI.Code,
 //          Date = DateTime.Now
 //       };
  //      _context.Emprestimo.Add(item);
  //      _context.Items.Remove(modelI);
 //       await _context.SaveChangesAsync();
 //       return Ok(modelI);
//    }
    
 //   [HttpPost("{id}  (Devolver)")]
 //   public async Task<ActionResult> GiveBack(int id)
//    {
  //      var modelL = await _context.Emprestimo.FindAsync(id);
 //       if (modelL == null) NotFound();
//        var item = new Items()
//        {
 //           Name = modelL.Name,
//            Code = modelL.Code,
 //           CategoryId = modelL.CategoryId
 //       };
//        _context.Items.Add(item);
 //       _context.Emprestimo.Remove(modelL);
//        await _context.SaveChangesAsync();
 //       return Ok(modelL);
 //   }

//    [HttpGet]
 //   public async Task<ActionResult> GetAll()
 //   {
//        var model = await _context.Emprestimo.ToListAsync();
//        return Ok(model);
 //   }
}