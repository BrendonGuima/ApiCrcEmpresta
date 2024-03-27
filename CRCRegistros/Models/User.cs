using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CRCRegistros.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public Perfil Perfil { get; set; }
}

public enum Perfil
{
    Administrador,
    Usuario
}