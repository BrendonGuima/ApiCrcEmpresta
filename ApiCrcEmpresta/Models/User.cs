using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ApiCrcEmpresta.Enums;

namespace ApiCrcEmpresta.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public string Password { get; set; }

        [BsonRequired]
        public Perfil Perfil { get; set; }
    }
}
