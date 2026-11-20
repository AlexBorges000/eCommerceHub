using BlazorShop.Models.DTOs.UserDtos.Registration;
using FluentValidation;

namespace BlazorShop.Api.Validations.Users.Registration;

public class BusinessUserRegistrationValidator : AbstractValidator<BusinessUserRegistrationRequestDto>
{
    public BusinessUserRegistrationValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .MaximumLength(250)
            .WithMessage("Comprimento inválido.")
            .EmailAddress()
            .WithMessage("E-mail inválido.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(8, 250)
            .WithMessage("A senha deve ter entre 8 e 250 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).+$")
            .WithMessage("A senha deve conter letras maiúsculas, minúsculas e números.");
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Equal(x => x.Password)
            .WithMessage("As senhas não coincidem.");
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(8, 20)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.TradeName)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.LegalName)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 150)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.PurchasingContact)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.StateRegistration)
            .MaximumLength(20)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.CNPJ)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Matches(@"^(?:[0-9A-Za-z]{12}[0-9]{2}|[0-9A-Za-z]{2}\.[0-9A-Za-z]{3}\.[0-9A-Za-z]{3}/[0-9A-Za-z]{4}-[0-9]{2})$")
            .WithMessage("CNPJ em formato inválido.");
    }
}

