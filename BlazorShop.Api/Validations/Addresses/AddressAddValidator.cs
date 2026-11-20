using BlazorShop.Models.DTOs.Address;
using FluentValidation;

namespace BlazorShop.Api.Validations.Addresses;

public class AddressAddValidator : AbstractValidator<AddressAddRequestDto>
{
    public AddressAddValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 250)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 10)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.Complement)
            .MaximumLength(250)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.District)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.UF)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Must(uf => new[] { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" }.Contains(uf))
            .WithMessage("UF inválida.");
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Matches(@"^[0-9]{5}-?[0-9]{3}$")
            .WithMessage("CEP em formato inválido.");
    }
}

