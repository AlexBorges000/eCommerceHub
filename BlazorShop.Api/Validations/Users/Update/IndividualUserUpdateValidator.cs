using BlazorShop.Models.DTOs.UserDtos.Update;
using FluentValidation;

namespace BlazorShop.Api.Validations.Users.Update;

public class IndividualUserUpdateValidator : AbstractValidator<IndividualUserUpdateRequestDto>
{
    public IndividualUserUpdateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Campo obrigatório.")
            .Length(8, 20)
            .WithMessage("Comprimento inválido.");
    }
}

