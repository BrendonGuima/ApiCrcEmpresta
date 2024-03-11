using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CRCRegistros.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CRCRegistros.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly MongoDbContext _context;
    
    public UsersController(MongoDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    { 
        var model = await _context.GetAllUsers();
        return Ok(model);
    }


   // [AllowAnonymous]
   // [HttpPost("authenticate")]
//    public async Task<ActionResult> Authenticate(AuthenticateDto model)
  //  {
  //      var userDb = await _context.Usuarios.(n => n.Name == model.Name);
  //      if (userDb == null || userDb.Password != model.Password) return Unauthorized();
//
 //       var jwt = JwtTokenGerenate(userDb);

//        return Ok(new { JwtToken = jwt });
 //   }

    private string JwtTokenGerenate(Users model)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("eyH31298ehR2wef329Rh923bF9923ED1");
        var claims = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
            new Claim(ClaimTypes.Role, model.Perfil.ToString())
        });

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}