using Microsoft.Extensions.Options;

using MongoDB.Driver;

using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Infrastructure;

public static class MongoDbIndexInitializer
{
    public static async Task CreateUniqueOfficeAddressIndex(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        var collection = database.GetCollection<Office>($"{typeof(Office).Name}s");

        var indexKeysDefinition = Builders<Office>.IndexKeys
            .Ascending(o => o.City)
            .Ascending(o => o.Street)
            .Ascending(o => o.HouseNumber);

        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<Office>(indexKeysDefinition, indexOptions);

        await collection.Indexes.CreateOneAsync(indexModel);
    }
}
