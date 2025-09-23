using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OfficesMicroService.Domain.Entities;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }
}
