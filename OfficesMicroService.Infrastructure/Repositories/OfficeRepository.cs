using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;

using OfficesMicroService.Application.DTOs;
using OfficesMicroService.Application.Interfaces.Repositories;
using OfficesMicroService.Domain.Entities;

namespace OfficesMicroService.Infrastructure.Repositories;

public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
{
    public OfficeRepository(IMongoClient client, IOptions<MongoDbSettings> settings) : base(client, settings) { }

    public async Task<IEnumerable<Office>> GetByCityAsync(string city, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Office>.Filter.Eq(o => o.City, city);
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<bool> PartialUpdateAsync(string id, UpdateDefinition<Office> update, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Office>.Filter.Eq(e => e.Id, id);
        var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
