using BlazorShop.Models.DTOs.TokenDto;
using FluentValidation;

namespace BlazorShop.Api.Validations.Auth;

public class RefreshTokenValidator : AbstractValidator<RequestRefreshTokenDto>
{
    public RefreshTokenValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Token obrigatório.");
    }
}

