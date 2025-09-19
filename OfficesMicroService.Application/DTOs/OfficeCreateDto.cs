using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;

namespace OfficesMicroService.Application.DTOs;

public class OfficeCreateDto
{
    public string? PhotoId { get; set; }

    [Required(ErrorMessage = "Please, enter the office's city")]
    public string City { get; set; }

    [Required(ErrorMessage = "Please, enter the office’s street")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Please, enter the office’s house number")]
    public string HouseNumber { get; set; }

    [Required(ErrorMessage = "Please, enter the office’s number")]
    public string OfficeNumber { get; set; }

    [Required(ErrorMessage = "Please, enter the phone number")]
    public string RegistryPhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;
}
