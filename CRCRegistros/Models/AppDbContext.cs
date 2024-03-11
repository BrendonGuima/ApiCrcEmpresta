using MongoDB.Driver;

namespace CRCRegistros.Models;

public class MongoDbContext
{
  private readonly IMongoDatabase _database;
  private readonly IMongoCollection<Users> _usersCollection;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("CrcEmpresta");
    }

    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }
    public IMongoCollection<Users> Usuarios => _database.GetCollection<Users>("Usuarios");

}