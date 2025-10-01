using FluentValidation;
using OfficesMicroService.Application.DTOs;

namespace OfficesMicroService.Application.Validators;

public class OfficeUpdateDtoValidator : AbstractValidator<OfficeUpdateDto>
{
    public OfficeUpdateDtoValidator()
    {
        RuleFor(o => o.City)
            .MaximumLength(100).WithMessage("City name is too long")
            .When(o => !string.IsNullOrEmpty(o.City));

        RuleFor(o => o.Street)
            .MaximumLength(100).WithMessage("Street name is too long")
            .When(o => !string.IsNullOrEmpty(o.Street));

        RuleFor(o => o.HouseNumber)
            .MaximumLength(20).WithMessage("House number is too long")
            .When(o => !string.IsNullOrEmpty(o.HouseNumber));

        RuleFor(o => o.OfficeNumber)
            .MaximumLength(20).WithMessage("Office number is too long")
            .When(o => !string.IsNullOrEmpty(o.OfficeNumber));

        RuleFor(o => o.RegistryPhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("You've entered an invalid phone number format")
            .When(o => !string.IsNullOrEmpty(o.RegistryPhoneNumber));
    }
}
