using BlazorShop.Models.DTOs.Address;
using FluentValidation;

namespace BlazorShop.Api.Validations.Addresses;

public class AddressUpdateValidator : AbstractValidator<AddressUpdateRequestDto>
{
    public AddressUpdateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Street)
            .MaximumLength(250)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.Number)
            .MaximumLength(10)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.Complement)
            .MaximumLength(250)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.City)
            .MaximumLength(100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.District)
            .MaximumLength(100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.PostalCode)
            .Matches(@"^[0-9]{5}-?[0-9]{3}$")
            .WithMessage("CEP em formato inválido.")
            .When(x => x.PostalCode is not null);
    }
}

