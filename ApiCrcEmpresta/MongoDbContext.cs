using ApiCrcEmpresta.Models;
using CRCRegistros.Models;
using MongoDB.Driver;

namespace ApiCrcEmpresta
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<User> Users => _database.GetCollection<User>("Usuarios");
        public IMongoCollection<Category> Category => _database.GetCollection<Category>("Category");
        public IMongoCollection<Item> Items => _database.GetCollection<Item>("Items");
        public IMongoCollection<ItemLending> ItemLending => _database.GetCollection<ItemLending>("ItemLending");
        public IMongoCollection<History> HistoryLendItems => _database.GetCollection<History>("HistoryLendItems");

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDB");
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase("CrcEmpresta");
        }
    }
}
