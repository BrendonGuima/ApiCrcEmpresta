using MongoDB.Driver;

namespace CRCRegistros.Models;

public class MongoDbContext
{
  private readonly IMongoDatabase _database;
  private readonly IMongoCollection<Users> _usersCollection;
  public IMongoCollection<Users> Users => _database.GetCollection<Users>("Usuarios");

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("CrcEmpresta");
        _usersCollection = _database.GetCollection<Users>("Usuarios");
    }

    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }
    public async Task Create(Users user)
    {
        await _usersCollection.InsertOneAsync(user);
    }

}