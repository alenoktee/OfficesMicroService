using MongoDB.Bson.Serialization.Attributes;

namespace OfficesMicroService.Domain.Entities;

public class Office : BaseEntity
{
    [BsonElement("photoId")]
    public string? PhotoId { get; set; }
    [BsonElement("city")]
    public string City { get; set; } = string.Empty;
    [BsonElement("street")]
    public string Street { get; set; } = string.Empty;
    [BsonElement("houseNumber")]
    public string HouseNumber { get; set; } = string.Empty;
    [BsonElement("officeNumber")]
    public string OfficeNumber { get; set; } = string.Empty;
    [BsonElement("registryPhoneNumber")]
    public string RegistryPhoneNumber { get; set; } = string.Empty;
    [BsonElement("isActive")]
    public bool IsActive { get; set; } = true;
}
