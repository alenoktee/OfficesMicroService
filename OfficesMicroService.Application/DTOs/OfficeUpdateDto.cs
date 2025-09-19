using System.ComponentModel.DataAnnotations;

namespace OfficesMicroService.Application.DTOs;

public class OfficeUpdateDto
{
    public string? PhotoId { get; set; }

    [Required(ErrorMessage = "Please, enter the office's city")]
    public required string City { get; set; }

    [Required(ErrorMessage = "Please, enter the office’s street")]
    public required string Street { get; set; }

    [Required(ErrorMessage = "Please, enter the office’s house number")]
    public required string HouseNumber { get; set; }

    [Required(ErrorMessage = "Please, enter the office’s number")]
    public required string OfficeNumber { get; set; }

    [Required(ErrorMessage = "Please, enter the phone number")]
    [Phone(ErrorMessage = "You've entered an invalid phone number")]
    public required string RegistryPhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;
}
