using BlazorShop.Models.DTOs.OrderDtos;
using FluentValidation;

namespace BlazorShop.Api.Validations.Order;

public class OrderItemAddValidator : AbstractValidator<OrderItemAddRequestDto>
{
    public OrderItemAddValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
            .WithMessage("O valor deve ser maior que zero.");
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .WithMessage("O valor deve ser maior que zero.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("O valor deve ser maior que zero.");
    }
}

