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

        public string Name { get; set; }

        public string Password { get; set; }

        public Perfil Perfil { get; set; }
    }
}
