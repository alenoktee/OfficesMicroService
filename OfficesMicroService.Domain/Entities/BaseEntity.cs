using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OfficesMicroService.Domain.Entities;

public abstract class BaseEntity
{
    [BsonId]
    public ObjectId Id { get; init; }
}
