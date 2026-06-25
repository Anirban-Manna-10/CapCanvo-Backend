using CapCanvo.Core.Entities;
using CapCanvo.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CapCanvo.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");

    public IMongoCollection<Board> Boards => _database.GetCollection<Board>("Boards");

    public IMongoCollection<BoardMember> BoardMembers => _database.GetCollection<BoardMember>("BoardMembers");

    public IMongoCollection<CanvasObject> CanvasObjects => _database.GetCollection<CanvasObject>("canvasObjects");
}