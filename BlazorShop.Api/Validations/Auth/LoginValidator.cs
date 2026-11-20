using BlazorShop.Models.DTOs.UserDtos.Login;
using FluentValidation;

namespace BlazorShop.Api.Validations.Auth;

public class LoginValidator : AbstractValidator<RequestLoginDto>
{
    public LoginValidator()
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
            .Length(1, 250)
            .WithMessage("Comprimento inválido.");
    }
}

