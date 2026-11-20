using BlazorShop.Models.DTOs.UserDtos.Update;
using FluentValidation;

namespace BlazorShop.Api.Validations.Users.Update;

public class UserPasswordUpdateValidator : AbstractValidator<UserPasswordUpdateRequestDto>
{
    public UserPasswordUpdateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(1, 250)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(8, 250)
            .WithMessage("A senha deve ter entre 8 e 250 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).+$")
            .WithMessage("A senha deve conter letras maiúsculas, minúsculas e números.");
        RuleFor(x => x.ConfirmedPassword)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Equal(x => x.NewPassword)
            .WithMessage("As senhas não coincidem.");
    }
}

