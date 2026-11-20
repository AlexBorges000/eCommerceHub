using BlazorShop.Models.DTOs.UserDtos.Update;
using FluentValidation;

namespace BlazorShop.Api.Validations.Users.Update;

public class BusinessUserUpdateValidator : AbstractValidator<RequestUpdateBusinessUserDto>
{
    public BusinessUserUpdateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.TradeName)
            .MaximumLength(100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.LegalName)
            .MaximumLength(150)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.PurchasingContact)
            .MaximumLength(100)
            .WithMessage("Comprimento inválido.");
        RuleFor(x => x.Phone)
            .Length(8, 20)
            .WithMessage("Telefone em formato inválido.")
            .When(x => x.Phone is not null);
    }
}

