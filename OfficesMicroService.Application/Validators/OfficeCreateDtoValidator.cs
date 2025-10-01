using FluentValidation;

using OfficesMicroService.Application.DTOs;

namespace OfficesMicroService.Application.Validators;

public class OfficeCreateDtoValidator : AbstractValidator<OfficeCreateDto>
{
    public OfficeCreateDtoValidator()
    {
        RuleFor(o => o.City)
            .NotEmpty().WithMessage("Please, enter the office's city")
            .MaximumLength(100).WithMessage("City name is too long");

        RuleFor(o => o.Street)
            .NotEmpty().WithMessage("Please, enter the office’s street")
            .MaximumLength(100).WithMessage("Street name is too long");

        RuleFor(o => o.HouseNumber)
            .NotEmpty().WithMessage("Please, enter the office’s house number")
            .MaximumLength(20).WithMessage("House number is too long");

        RuleFor(o => o.OfficeNumber)
            .MaximumLength(20).WithMessage("Office number is too long");

        RuleFor(o => o.RegistryPhoneNumber)
            .NotEmpty().WithMessage("Please, enter the phone number")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("You've entered an invalid phone number format");

        RuleFor(o => o.IsActive)
            .NotNull().WithMessage("Please, specify the office status");
    }
}
