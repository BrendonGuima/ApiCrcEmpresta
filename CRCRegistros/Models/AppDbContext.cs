using MongoDB.Driver;

namespace CRCRegistros.Models;

public class MongoDbContext
{
  private readonly IMongoDatabase _database;
  private readonly IMongoCollection<Users> _usersCollection;
  private readonly IMongoCollection<Items> _itemsCollection;
  private readonly IMongoCollection<Category> _categoryCollection;
  public IMongoCollection<Users> Users => _database.GetCollection<Users>("Usuarios");
  public IMongoCollection<Items> Items => _database.GetCollection<Items>("Items");
  public IMongoCollection<Category> Category => _database.GetCollection<Category>("Category");

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase("CrcEmpresta");
        _usersCollection = _database.GetCollection<Users>("Usuarios");
        _itemsCollection = _database.GetCollection<Items>("Items");
        _categoryCollection = _database.GetCollection<Category>("Category");
    }
    
    //Users
    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }
    public async Task Create(Users user)
    {
        await _usersCollection.InsertOneAsync(user);
    }
    
    //Items
    public async Task<IEnumerable<Items>> GetAllItems()
    {
        return await _itemsCollection.Find(_ => true).ToListAsync();
    }
    
    //Category
    public async Task CreateCategory(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
    }

    public async Task<IEnumerable<Category>> GetAllCategorys()
    {
        return await _categoryCollection.Find(_ => true).ToListAsync();
    }

}