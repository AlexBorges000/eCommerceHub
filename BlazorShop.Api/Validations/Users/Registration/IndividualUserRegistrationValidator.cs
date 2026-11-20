using BlazorShop.Models.DTOs.UserDtos.Registration;
using FluentValidation;

namespace BlazorShop.Api.Validations.Users.Registration;

public class IndividualUserRegistrationValidator : AbstractValidator<IndividualUserRegistrationRequestDto>
{
    public IndividualUserRegistrationValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(3, 100)
            .WithMessage("Comprimento inválido.");
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
        RuleFor(x => x.CPF)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Matches(@"^(?:[0-9]{11}|[0-9]{3}\.[0-9]{3}\.[0-9]{3}-[0-9]{2})$")
            .WithMessage("CPF em formato inválido.");
    }
}

