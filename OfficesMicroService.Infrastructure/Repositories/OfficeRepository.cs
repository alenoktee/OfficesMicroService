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

    public async Task<bool> DoesAddressExistAsync(string city, string street, string houseNumber, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Office>.Filter.And(
            Builders<Office>.Filter.Eq(o => o.City, city),
            Builders<Office>.Filter.Eq(o => o.Street, street),
            Builders<Office>.Filter.Eq(o => o.HouseNumber, houseNumber)
        );
        return await _collection.Find(filter).AnyAsync(cancellationToken);
    }

    public async Task<bool> UpdateDetailsAsync(string id, OfficeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        if (!ObjectId.TryParse(id, out var objectId))
        {
            return false;
        }

        var filter = Builders<Office>.Filter.Eq(o => o.Id, objectId);

        var update = Builders<Office>.Update
            .Set(o => o.PhotoId, dto.PhotoId)
            .Set(o => o.City, dto.City)
            .Set(o => o.Street, dto.Street)
            .Set(o => o.HouseNumber, dto.HouseNumber)
            .Set(o => o.OfficeNumber, dto.OfficeNumber)
            .Set(o => o.RegistryPhoneNumber, dto.RegistryPhoneNumber)
            .Set(o => o.IsActive, dto.IsActive);

        var result = await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
