namespace OfficesMicroService.Application.DTOs;

public class OfficeDto
{
    public string Id { get; set; } = string.Empty;
    public string? PhotoId { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string HouseNumber { get; set; } = string.Empty;
    public string OfficeNumber { get; set; } = string.Empty;
    public string RegistryPhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
