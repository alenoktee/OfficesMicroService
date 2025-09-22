using System.ComponentModel.DataAnnotations;

namespace OfficesMicroService.Application.DTOs;

public class OfficeUpdateDto
{
    public string? PhotoId { get; set; }

    [Required(ErrorMessage = "Please, enter the office's city")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please, enter the office’s street")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please, enter the office’s house number")]
    public string HouseNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please, enter the office’s number")]
    public string OfficeNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please, enter the phone number")]
    [Phone(ErrorMessage = "You've entered an invalid phone number")]
    public string RegistryPhoneNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
