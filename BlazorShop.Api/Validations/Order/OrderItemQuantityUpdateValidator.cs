using BlazorShop.Models.DTOs.OrderDtos;
using FluentValidation;

namespace BlazorShop.Api.Validations.Order;

public class OrderItemQuantityUpdateValidator : AbstractValidator<OrderItemQuantityUpdateRequestDto>
{
    public OrderItemQuantityUpdateValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        // O item é identificado pelo parâmetro da rota; OrderItemId não é usado pelo endpoint.
        RuleFor(x => x.OrderItemId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Identificador inválido.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero.");
    }
}

